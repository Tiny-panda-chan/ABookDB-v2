using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    internal class UserRepository : Repository<UserModel>, Models.Interfaces.IUserRepository
    {
        private readonly ABookDBContext _context;
        public UserRepository(ABookDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<BookModel>> GetAllReadBooksAsync(UserModel model)
        {
            return await _context.ReadBooks.Include(b => b.Book).Include(u => u.User).Where(u => u.User == model).Select(b => b.Book).ToListAsync();
        }

        public async Task<UserModel?> GetByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
        }
    }
}
