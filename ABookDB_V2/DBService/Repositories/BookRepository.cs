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

        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<CategoryModel>?> GetAllCategoriesAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Categories).LoadAsync();
            if (model.Categories.IsNullOrEmpty())
                return new List<CategoryModel>();
            return model.Categories;
        }

        public async Task<IEnumerable<FileModel>?> GetAllFilesAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.BookFiles).LoadAsync();
            if (model.BookFiles.IsNullOrEmpty())
                return new List<FileModel>();
            return model.BookFiles;
        }

        public async Task<IEnumerable<ReviewModel>?> GetAllReviewsAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Reviews).LoadAsync();
            if (model.Reviews.IsNullOrEmpty())
                return new List<ReviewModel>();
            return model.Reviews;
        }

        public async Task<IEnumerable<UrlModel>?> GetAllUrlsAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Urls).LoadAsync();
            if (model.Urls.IsNullOrEmpty())
                return new List<UrlModel>();
            return model.Urls;
        }


    }
}
