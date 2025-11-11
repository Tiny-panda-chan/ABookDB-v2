using Microsoft.EntityFrameworkCore;
using Models.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    public class CategoryRepository : Repository<CategoryModel>, Models.Interfaces.ICategoryRepository
    {
        private readonly ABookDBContext _context;
        public CategoryRepository(ABookDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetAllBooksInCategory(CategoryModel model)
        {
            return await _context.Books.Include(u => u.Categories).Where(b => b.Categories.Any(c => c == model)).ToListAsync();
        }

        public async Task<CategoryModel?> GetByNameAsync(string name)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.Name == name);
        }
    }
}
