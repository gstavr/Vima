using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vimachem.Data;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;

namespace Vimachem.Repository
{
    public class Repository<T> : IRepository<T> where T :class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            
            if (dbSet.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool track = true)
        {
            IQueryable<T> query = dbSet;

            if (!track)
            {
                query = query.AsNoTracking();
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool track = true, params string[] includePaths)
        {
            IQueryable<T> query = dbSet;

            if (!track)
            {
                query = query.AsNoTracking();
            }

            if (includePaths != null)
            {

                foreach (var path in includePaths)
                {
                    query = query.Include(path);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params string[] includePaths)
        {
            IQueryable<T> query = dbSet;

            
            if (includePaths != null)
            {
                
                foreach (var path in includePaths)
                {
                    query = query.Include(path);
                }
            }

            // Apply the filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}
