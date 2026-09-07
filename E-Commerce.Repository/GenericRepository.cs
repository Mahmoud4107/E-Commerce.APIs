using E_Commerce.Core.Entities;
using E_Commerce.Core.RepostriesContruct;
using E_Commerce.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if(typeof(T) == typeof(Product))  // We Use Specification Instead
            //    return (IEnumerable<T>) await _context.Products.Include(P => P.Brand).Include(P => P.Category).ToListAsync();

            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            //if (typeof(T) == typeof(Product))  // We Use Specification Instead
            //    return await _context.Products.Where(P => P.Id ==id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithSpec(ISpecification<T> spec)
        {
           return await SpecificationAvaluator<T>.GetQuery(_context.Set<T>(), spec).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsyncWithSpec(ISpecification<T> spec)
        {
            return await SpecificationAvaluator<T>.GetQuery(_context.Set<T>(),spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> specification)
        {
           return await SpecificationAvaluator<T>.GetQuery(_context.Set<T>(),specification).CountAsync();
        }
    }
}
