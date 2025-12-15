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
        public BookRepository(ABookDBContext context) : base(context)
        {
        }

        public new async Task<BookModel?> GetByIdAsync(int id)
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
            CategoryRepository cr = new CategoryRepository(_context);
            var catBooks = new List<BookModel>();
            foreach (var category in searchCategories)
            {
                var cat = await cr.GetByNameAsync(category);
                if (cat != null)
                    catBooks.AddRange(await cr.GetAllBooksInCategory(cat));
            }
            return catBooks;
        }

        public async Task<IEnumerable<CategoryModel>?> GetAllCategoriesAsync(BookModel model)
        {
            try
            {
                await _context.Entry(model).Collection(c => c.Categories).LoadAsync();
                return model.Categories;
            }
            catch (Exception)
            {
                return new List<CategoryModel>();
            }
        }

        public async Task<IEnumerable<FileModel>?> GetAllFilesAsync(BookModel model)
        {
            try
            {
                await _context.Entry(model).Collection(c => c.BookFiles).LoadAsync();
                return model.BookFiles;
            }
            catch (Exception)
            {
                return new List<FileModel>();
            }
        }

        public async Task<IEnumerable<ReviewModel>?> GetAllReviewsAsync(BookModel model)
        {
            try
            {
                await _context.Entry(model).Collection(c => c.Reviews).Query().Include(u => u.createdBy).LoadAsync();
                return model.Reviews;
            }
            catch (Exception)
            {
                return new List<ReviewModel>();
            }
        }

        public async Task<IEnumerable<UrlModel>?> GetAllUrlsAsync(BookModel model)
        {
            try
            {
                await _context.Entry(model).Collection(c => c.Urls).LoadAsync();
                return model.Urls;
            }
            catch (Exception)
            {
                return new List<UrlModel>();
            }
        }


    }
}
