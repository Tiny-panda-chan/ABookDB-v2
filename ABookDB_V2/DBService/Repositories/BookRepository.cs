using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Models;

namespace DBService.Repositories
{
    public class BookRepository : Repository<BookModel>, Models.Interfaces.IBookRepository
    {
        private readonly ABookDBContext _context;
        public BookRepository(ABookDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BookModel?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(c => c.CreatedBy).Include(a => a.Author).SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            return await _context.Books.Include(u => u.CreatedBy).Include(a => a.Author).ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetAllAsyncByString(string searchString)
        {
            return await _context.Books.Include(u => u.CreatedBy).Include(a => a.Author).Where(b => b.Name.Contains(searchString)).ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetAllAsyncByCategories(List<string> searchCategories)
        {
            return await _context.Books.Include(u => u.CreatedBy).Include(c => c.Categories).Include(a => a.Author).Where(b => b.Categories.Any(c => searchCategories.Contains(c.Name))).ToListAsync();
        }

        public async Task<IEnumerable<CategoryModel>?> GetAllCategoriesAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Categories).LoadAsync();
            if (model.Categories is null)
                return new List<CategoryModel>();
            return model.Categories;
        }

        public async Task<IEnumerable<FileModel>?> GetAllFilesAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.BookFiles).LoadAsync();
            if (model.BookFiles is null)
                return new List<FileModel>();
            return model.BookFiles;
        }

        public async Task<IEnumerable<ReviewModel>?> GetAllReviewsAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Reviews).Query().Include(u => u.createdBy).LoadAsync();
            if (model.Reviews is null)
                return new List<ReviewModel>();
            return model.Reviews;
        }

        public async Task<IEnumerable<UrlModel>?> GetAllUrlsAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Urls).LoadAsync();
            if (model.Urls is null)
                return new List<UrlModel>();
            return model.Urls;
        }


    }
}
