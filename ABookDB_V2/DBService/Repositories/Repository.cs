using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    internal abstract class Repository<TEntity>
        where TEntity : class
    {
        protected readonly ABookDBContext _context;

        protected Repository(ABookDBContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            _context.Update(entity);
        }
    }
}
