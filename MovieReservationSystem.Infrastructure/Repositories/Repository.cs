using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Infrastructure.Data;
using System.Linq.Expressions;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // inject DI, internal DbSet<T>
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        private static readonly char[] separator = [','];

        public Repository(AppDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property.Trim());
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property.Trim());
                }
            }

            return [.. query];
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
