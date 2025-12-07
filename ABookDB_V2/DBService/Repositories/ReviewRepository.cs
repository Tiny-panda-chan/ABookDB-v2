using Microsoft.EntityFrameworkCore;
using Models.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    public class ReviewRepository : Repository<ReviewModel>, Models.Interfaces.IReviewRepository
    {
        public ReviewRepository(ABookDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReviewModel>> GetAllAsync()
        {
            return await _context.Reviews.Include(u => u.createdBy).ToListAsync();
        }

        public new async Task<ReviewModel?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(u => u.createdBy).SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
