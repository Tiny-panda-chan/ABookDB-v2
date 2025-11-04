using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    public abstract class Repository<TEntity>
        where TEntity : Models.Models.RepositoryEntity
    {
        protected readonly ABookDBContext _context;

        protected Repository(ABookDBContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Edit(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
