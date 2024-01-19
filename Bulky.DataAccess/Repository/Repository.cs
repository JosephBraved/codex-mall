using Bulky.DataAccess.Repository.iRepository;
using Bulky.Models.Models;
using BulkyDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
			_db = db;
			this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
		{
			dbSet.Add(entity);
			
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeproperties = null, bool tracked = false)
		{
			if (tracked) {
                IQueryable<T> query = dbSet;
                query = query.Where(filter);
                if (!string.IsNullOrEmpty(includeproperties))
                {
                    foreach (var includeprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query.Include(includeprop);
                    }

                }
                return query.FirstOrDefault();
            }
			else
			{
                IQueryable<T> query = dbSet.AsNoTracking();
                query = query.Where(filter);
                if (!string.IsNullOrEmpty(includeproperties))
                {
                    foreach (var includeprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query.Include(includeprop);
                    }

                }
                return query.FirstOrDefault();
            }
			
			
		}

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeproperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeproperties))
            {
                foreach (var includeprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
		{
			dbSet.Add(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
