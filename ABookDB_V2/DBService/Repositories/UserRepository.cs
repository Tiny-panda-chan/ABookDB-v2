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
    public class UserRepository : Repository<UserModel>, Models.Interfaces.IUserRepository
    {
        private readonly ABookDBContext _context;
        public UserRepository(ABookDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetAllBookCreatedByAsync(UserModel model)
        {
            return await _context.Books.Include(u => u.CreatedBy).Where(b => b.CreatedBy == model).ToListAsync();
        }

        public async Task<IEnumerable<ReadBooksModel>> GetAllReadBooksAsync(UserModel model)
        {
            return await _context.ReadBooks.Include(b => b.Book).Include(u => u.User).Where(u => u.User == model).ToListAsync();
        }
    }
}
