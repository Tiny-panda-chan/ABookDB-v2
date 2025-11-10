using AutoMapper;
using DBService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebProject.ViewModels.Book;
using WebProject.ViewModels.Review;
using WebProject.ViewModels.User;
using static WebScraper.ScrapedFileModel;

namespace WebProject.ModelTranslator
{
    public class ModelTranslator(ABookDBContext _context, IMapper _mapper, IHttpContextAccessor _httpContextAccesor) : IModelTranslator
    {
        private readonly ABookDBContext _context;
        private AuthorRepository AuthorRepository = new AuthorRepository(_context);
        private BookRepository BookRepository = new BookRepository(_context);
        private CategoryRepository CategoryRepository = new CategoryRepository(_context);
        private UserRepository UserRepository = new UserRepository(_context);
        private ReviewRepository ReviewRepository = new ReviewRepository(_context);


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
            obj = _mapper.Map<EditVM>(bk);

            return obj;
        }

        public async Task<ViewModels.Book.CreateVM> FillObjectAsync(ViewModels.Book.CreateVM obj)
        {
            obj.CategoryCreateVM = await FillObjectAsync(new ViewModels.Category.CreateVM()); //= (await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
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

        //User
        public Task<ProfileVM> FillObjectAsync(ProfileVM obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveObjectAsync(ViewModels.User.ReadBookVM obj)
        {
            UserModel user = await GetUser();
            BookModel book = await BookRepository.GetByIdAsync(obj.BookID);
            if (book == null || user == null)
                return false;
            ReadBooksModel rb = new ReadBooksModel();
            rb.Book = book;
            rb.User = user;
            rb.Page = obj.ReadToPage;
            rb.ReadStage = (obj.ReadToPage < book.TotalPages) ? ReadStage.InProgress : ReadStage.Finished;
            UserRepository.AddOrUpdateReadBook(rb);
            return true;
        }

        //Review
        public async Task<ListVM> FillObjectAsync(ListVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
            if (bk == null)
                return null;
            await BookRepository.GetAllReviewsAsync(bk);
            if (bk.Reviews == null)
                return null;
            obj.ReviewItems = bk.Reviews.Select(c => new ListVM.ReviewItem() { TextContent = c.Text, CreatedBy = c.createdBy.Username, CreatedOn = c.createdOn }).ToList();
            return obj;
        }

        public async Task<int> SaveObjectAsync(ViewModels.Review.CreateVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj.BookId);
            if (bk == null)
                return 0;
            UserModel user = await GetUser();
            ReviewModel rm = new ReviewModel() { createdOn = DateTime.Now, Text = obj.Text, book = bk, createdBy = user};
            
            ReviewRepository.Add(rm);
            return rm.Id;
        }

        //Category
        public async Task<ViewModels.Category.CreateVM> FillObjectAsync(ViewModels.Category.CreateVM obj)
        {
            obj.AllCategories = new((await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList());
            return obj;
        }


        public async Task<bool> SaveObjectAsync(ViewModels.Category.CreateVM obj)
        {
            CategoryModel cm = new CategoryModel() { Name = obj.Name };

            return CategoryRepository.Add(cm).GetValueOrDefault();
        }






        private async Task<UserModel?> GetUser()
        {
            UserModel user = await UserRepository.GetByIdAsync(Int32.Parse(_httpContextAccesor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value));
            if (user == null)
                return new UserModel();
            return user;
        }
    }
}
