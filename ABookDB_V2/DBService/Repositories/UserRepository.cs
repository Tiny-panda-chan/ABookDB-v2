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
        public UserRepository(ABookDBContext context) : base(context)
        {
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

        public async Task<ReadBooksModel?> GetReadByBookId(UserModel model, int bookId)
        {
            var rb = await _context.ReadBooks.Include(b => b.Book).Include(u => u.User).Where(u => u.User == model && u.Book.Id == bookId).SingleOrDefaultAsync();
            if (rb == null)
                return new ReadBooksModel();
            return (rb);
        }

        public void AddOrUpdateReadBook(ReadBooksModel model)
        {
            ReadBooksModel? rb = (_context.ReadBooks.FirstOrDefault(b => b.User == model.User && b.Book == model.Book));
            if (rb == null)
            {
                _context.ReadBooks.Add(model);
            }
            else
            {
                rb.Page = model.Page;
                rb.ReadStage = model.ReadStage;
                _context.ReadBooks.Update(rb);
            }
            _context.SaveChanges();
        }
    }
}
