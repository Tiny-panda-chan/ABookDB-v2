using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DBService.Repositories
{
    internal class BookRepository : Repository<BookModel>, Models.Interfaces.IBookRepository
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

        public Task<BookModel?> GetByIdAsync(int id)
        {
            return _context.Books.SingleOrDefaultAsync(book => book.Id == id);
        }


    }
}
