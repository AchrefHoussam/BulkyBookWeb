using BulkyBook.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> dbset;
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbset = _context.Set<T>() ;
        }

        public void Add(T item)
        {
            dbset.Add(item) ;
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbset;
            return query.ToList();
        }

        public T getFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
    }
}
