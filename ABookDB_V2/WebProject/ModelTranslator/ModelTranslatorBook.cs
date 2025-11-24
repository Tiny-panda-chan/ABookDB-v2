using AutoMapper;
using DBService.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;
using Models.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebProject.Helpers;
using WebProject.ViewModels.Book;
using WebProject.ViewModels.Review;
using WebProject.ViewModels.User;
using static WebScraper.ScrapedFileModel;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorBook(ABookDBContext _context,
        IStatusService _services,
        IHttpContextAccessor _httpContextAccesor,
        IBookRepository _bookRepository,
        IUserRepository _userRepository,
        ICategoryRepository _categoryRepository,
        IAuthorRepository _authorRepository) : ModelTranslatorParent(_httpContextAccesor, _context, _userRepository), IModelTranslatorBook
    {
        //Books
        public async Task<IndexVM> FillObjectAsync(IndexVM obj)
        {
            var books = await _bookRepository.GetAllAsync();
            UserModel user = await GetUser();
            obj.BookList = books.Select(bl => new IndexVM.BookItem
            {
                Id = bl.Id,
                Title = bl.Name,
                Description = bl.Description,
                BookCategories = _bookRepository.GetAllCategoriesAsync(bl).Result.Select(c => c.Name)?.ToList(),
                CreatedById = bl.CreatedBy.Id,
                UserProgress = (_userRepository.GetReadByBookId(user, bl.Id))?.Result?.ReadStage
            }).ToList();
            obj.Categories = (await _categoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }

        public async Task<IndexVM> FillObjectAsync(IndexVM obj, string searchString, List<string> categories)
        {
            UserModel user = await GetUser();
            obj.BookList = new();
            if (searchString != null)
            {
                var books = await _bookRepository.GetAllAsyncByString(searchString);
                obj.BookList.AddRange(books.Select(bl => new IndexVM.BookItem
                {
                    Id = bl.Id,
                    Title = bl.Name,
                    Description = bl.Description,
                    BookCategories = _bookRepository.GetAllCategoriesAsync(bl)?.Result?.Select(c => c.Name)?.ToList(),
                    CreatedById = bl.CreatedBy?.Id ?? 0,
                    UserProgress = (_userRepository.GetReadByBookId(user, bl.Id))?.Result?.ReadStage
                }).ToList());
            }

            if (categories != null)
            {
                var books = await _bookRepository.GetAllAsyncByCategories(categories);
                obj.BookList?.AddRange(books.Select(bl => new IndexVM.BookItem
                {
                    Id = bl.Id,
                    Title = bl.Name,
                    Description = bl.Description,
                    BookCategories = bl.Categories?.Select(c => c.Name)?.ToList(),//because cats are already included in getallasyncbycategories query
                    CreatedById = bl.CreatedBy?.Id ?? 0,
                    UserProgress = (_userRepository.GetReadByBookId(user, bl.Id))?.Result?.ReadStage
                }).ToList());
            }
            
            obj.Categories = (await _categoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }



        public async Task<DetailVM> FillObjectAsync(DetailVM obj)
        {
            BookModel bk = await _bookRepository.GetByIdAsync(obj._id);
            obj.Name = bk.Name;
            obj.Description = bk.Description;
            obj.BookCategories = (await _bookRepository.GetAllCategoriesAsync(bk))?.Select(bc => bc.Name).ToList();
            obj.BookFiles = (await _bookRepository.GetAllFilesAsync(bk))?.Select(bc => bc.Name).ToList();
            obj.TotalPages = bk.TotalPages;
            obj.CreatedDate = bk.CreatedOn;
            UserModel user = await GetUser();
            if (user != null)
            {
                ReadBooksModel rbm = _userRepository.GetReadByBookId(user, obj._id)?.Result ?? new ReadBooksModel();
                if (rbm != null)
                    obj.ReadToPage = rbm.Page;
            }
            //obj = _mapper.Map<DetailVM>(bk);
            return obj;
        }

        public async Task<EditVM> FillObjectAsync(EditVM obj)
        {
            BookModel bk = await _bookRepository.GetByIdAsync(obj._id);
            await _bookRepository.GetAllCategoriesAsync(bk);
            await _bookRepository.GetAllFilesAsync(bk);
            await _bookRepository.GetAllUrlsAsync(bk);
            //obj = _mapper.Map<EditVM>(bk);

            return obj;
        }

        public async Task<ViewModels.Book.CreateVM> FillObjectAsync(ViewModels.Book.CreateVM obj)
        {
            obj.CategoryCreateVM = await _services.GetService<IModelTranslatorCategory>().FillObjectAsync(new ViewModels.Category.CreateVM()); //= (await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            obj.AllAuthors = (await _authorRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }


        public async Task<int> SaveObjectAsync(EditVM obj)
        {
            BookModel bk = await _bookRepository.GetByIdAsync(obj._id);
            //return saved object id for redirection
            return bk.Id;
        }

        public async Task<int> SaveObjectAsync(ViewModels.Book.CreateVM obj)
        {
            BookModel bk = new BookModel();
            bk.Name = obj.Name;
            bk.TotalPages = obj.TotalPages;
            bk.Description = obj.Description;
            bk.CreatedBy = await GetUser();
            
            foreach (var cat in obj.SelectedCategories ?? new List<string>())
            {
                if (bk.Categories == null)
                    bk.Categories = new List<CategoryModel>();
                var dbCat = await _categoryRepository.GetByNameAsync(cat);
                if (dbCat != null)
                    bk.Categories.Add(dbCat);
            }

            List<FileModel> files = new List<FileModel>();
            if (obj.UploadedFiles != null)
            {
                foreach (var file in obj.UploadedFiles)
                {
                    using (var memStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memStream);
                        var ci = new FileModel
                        {
                            Data = memStream.ToArray(),
                            Name = file.FileName,
                            FileType = Path.GetExtension(file.FileName)
                        };
                        files.Add(ci);
                    }
                }
            }
            bk.BookFiles = files;
            _bookRepository.Add(bk);
            //return saved object id for redirection
            return bk.Id;
        }

        public async Task<bool> SaveObjectAsync(ViewModels.Book.DeleteVM obj)
        {
            UserModel user = await GetUser();
            BookModel book = await _bookRepository.GetByIdAsync(obj.Id);
            if (book == null || user == null)
                return false;
            if (book.CreatedBy != user)
                return false;

            _bookRepository.Delete(book);
            return true;
        }

        public async Task<ViewModels.Book.DownloadFileVM> FillObjectAsync(int bookId, ViewModels.Book.DownloadFileVM obj)
        {
            if (obj.FileName == null || obj.FileName.Length == 0 || bookId == null)
                return obj;
            BookModel book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
                return obj;
            FileModel fm = (await _bookRepository.GetAllFilesAsync(book))?.FirstOrDefault(f => f.Name == obj.FileName);
            if (fm != null)
                obj.Data = fm.Data;
            return obj;
        }
    }
}
