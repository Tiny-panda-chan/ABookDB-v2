using AutoMapper;
using DBService.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Threading.Tasks;
using WebProject.ViewModels.Book;
using WebProject.ViewModels.User;

namespace WebProject.ModelTranslator
{
    public class ModelTranslator(ABookDBContext _context, IMapper _mapper) : IModelTranslator
    {
        private readonly ABookDBContext _context;
        private AuthorRepository AuthorRepository = new AuthorRepository(_context);
        private BookRepository BookRepository = new BookRepository(_context);
        private CategoryRepository CategoryRepository = new CategoryRepository(_context);
        private UserRepository UserRepository = new UserRepository(_context);


        //Books
        public async Task<IndexVM> FillObjectAsync(IndexVM obj)
        {
            var books = await BookRepository.GetAllAsync();
            obj.BookList = books.Select(bl => new IndexVM.BookItem
            {
                Id = bl.Id,
                Title = bl.Name,
                Description = bl.Description,
                //BookCategories = (BookRepository.GetAllCategoriesAsync(bl).Result == null ? new List<string>() : BookRepository.GetAllCategoriesAsync(bl).Result.Select(c => c.Name).ToList() )
                BookCategories = BookRepository.GetAllCategoriesAsync(bl).Result.Select(c => c.Name).ToList()
            }).ToList();
            obj.Categories = (await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }

        public async Task<DetailVM> FillObjectAsync(DetailVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
            obj.Name = bk.Name;
            obj.BookCategories = (await BookRepository.GetAllCategoriesAsync(bk)).Select(bc =>bc.Name).ToList();
            var dvm = _mapper.Map<DetailVM>(bk);
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

        public async Task<CreateVM> FillObjectAsync(CreateVM obj)
        {
            obj.AllCategories = (await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList();
            obj.AllAuthors = (await AuthorRepository.GetAllAsync()).Select(c => c.Name).ToList();
            return obj;
        }

        public Task<ProfileVM> FillObjectAsync(ProfileVM obj)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveObjectAsync(EditVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
            //return saved object id for redirection
            return bk.Id;
        }

        public async Task<int> SaveObjectAsync(CreateVM obj)
        {
            BookModel bk = new BookModel();
            BookRepository.Add(bk);
            //return saved object id for redirection
            return bk.Id;
        }
    }
}
