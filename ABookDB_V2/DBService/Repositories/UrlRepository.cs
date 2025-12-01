using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    public class UrlRepository : Repository<UrlModel>, Models.Interfaces.IUrlRepository
    {
        public UrlRepository(ABookDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UrlModel>> GetAllAsync()
        {
            return await _context.BookUrls.ToListAsync();
        }
    }
}
