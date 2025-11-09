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

        public bool? Add(TEntity entity)
        {
            if (_context.Set<TEntity>().Any(e => e == entity))
                return false;
            _context.Add(entity);
            _context.SaveChanges();
            return true;
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

        public bool Exists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
