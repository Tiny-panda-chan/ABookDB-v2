using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    public class AuthorRepository : Repository<AuthorModel>, Models.Interfaces.IAuthorRepository
    {
        public AuthorRepository(ABookDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetAllBooksByAuthorAsync(AuthorModel model)
        {
            return await _context.Books.Include(a => a.Author).Where(b => b.Author == model).ToListAsync();
        }
    }
}
