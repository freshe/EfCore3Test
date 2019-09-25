using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EfCore3Test.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace EfCore3Test.Db
{
    public class Repository<TEntity> where TEntity : class
    {
        private readonly TestDbContext _context;
        
        public Repository(TestDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            List<TEntity> list;

            if (filter != null)
                list = await _context.Set<TEntity>().Where(filter).ToListAsync();
            else
                list = await _context.Set<TEntity>().ToListAsync();

            return list;
        }

        public virtual async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public virtual void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _context.Set<TEntity>().Attach(entityToDelete);

            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}