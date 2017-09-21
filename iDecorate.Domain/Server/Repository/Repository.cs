using iDecorate.Data.Context;
using iDecorate.Domain.Server.Contract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace iDecorate.Domain.Server.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;

        public Repository(ApplicationDbContext context) => _context = context;

        public void Insert(T entity)
        {
            using (_context)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (_context)
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (_context)
            {
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
        public T Find(int key) => _context.Set<T>().Find(key);

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
    }
}
