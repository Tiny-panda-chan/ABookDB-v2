using AutoMapper;
using DBService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class ModelTranslatorBook(ABookDBContext _context, IStatusService _services, IHttpContextAccessor _httpContextAccesor) : ModelTranslatorParent(_httpContextAccesor, _context), IModelTranslatorBook
    {
        //Books
        public async Task<IndexVM> FillObjectAsync(IndexVM obj)
        {
            var books = await BookRepository.GetAllAsync();
            UserModel user = await GetUser();
            obj.BookList = books.Select(bl => new IndexVM.BookItem
            {
                Id = bl.Id,
                Title = bl.Name,
                Description = bl.Description,
                BookCategories = BookRepository.GetAllCategoriesAsync(bl).Result.Select(c => c.Name)?.ToList(),
                CreatedById = bl.CreatedBy.Id,
                UserProgress = (UserRepository.GetReadByBookId(user, bl.Id))?.Result?.ReadStage
            }).ToList();
            obj.Categories = (await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }

        public async Task<DetailVM> FillObjectAsync(DetailVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
            obj.Name = bk.Name;
            obj.Description = bk.Description;
            obj.BookCategories = (await BookRepository.GetAllCategoriesAsync(bk))?.Select(bc =>bc.Name).ToList();
            obj.BookFiles = (await BookRepository.GetAllFilesAsync(bk))?.Select(bc => bc.Name).ToList();
            obj.TotalPages = bk.TotalPages;
            obj.CreatedDate = bk.CreatedOn; 
            UserModel user = await GetUser();
            if (user != null)
            {
                ReadBooksModel rbm = UserRepository.GetReadByBookId(user, obj._id)?.Result;
                if (rbm != null)
                    obj.ReadToPage = rbm.Page;
            }
            //obj = _mapper.Map<DetailVM>(bk);
            return obj;
        }

        public async Task<EditVM> FillObjectAsync(EditVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
            await BookRepository.GetAllCategoriesAsync(bk);
            await BookRepository.GetAllFilesAsync(bk);
            await BookRepository.GetAllUrlsAsync(bk);
            //obj = _mapper.Map<EditVM>(bk);

            return obj;
        }

        public async Task<ViewModels.Book.CreateVM> FillObjectAsync(ViewModels.Book.CreateVM obj)
        {
            obj.CategoryCreateVM = await _services.GetService<ModelTranslatorCategory>().FillObjectAsync(new ViewModels.Category.CreateVM()); //= (await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            obj.AllAuthors = (await AuthorRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }


        public async Task<int> SaveObjectAsync(EditVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
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
            BookRepository.Add(bk);
            //return saved object id for redirection
            return bk.Id;
        }

        public async Task<bool> SaveObjectAsync(ViewModels.Book.DeleteVM obj)
        {
            UserModel user = await GetUser();
            BookModel book = await BookRepository.GetByIdAsync(obj.Id);
            if (book == null || user  == null)
                return false;
            if (book.CreatedBy != user)
                return false;

            BookRepository.Delete(book);
            return true;
        }
    }
}
