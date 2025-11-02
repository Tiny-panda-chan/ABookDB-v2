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
    internal class CategoryRepository : Repository<CategoryModel>, Models.Interfaces.ICategoryRepository
    {
        private readonly ABookDBContext _context;
        public CategoryRepository(ABookDBContext context) : base(context)
        {
            _context = context;
        }

        public Task<CategoryModel?> GetByIdAsync(int id)
        {
            return _context.Categories.SingleOrDefaultAsync(book => book.Id == id);
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
