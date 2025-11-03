using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return model.Categories;
        }

        public async Task<IEnumerable<FileModel>?> GetAllFilesAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.BookFiles).LoadAsync();
            return model.BookFiles;
        }

        public async Task<IEnumerable<ReviewModel>?> GetAllReviewsAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Comments).LoadAsync();
            return model.Comments;
        }

        public async Task<IEnumerable<UrlModel>?> GetAllUrlsAsync(BookModel model)
        {
            await _context.Entry(model).Collection(c => c.Urls).LoadAsync();
            return model.Urls;
        }


    }
}
