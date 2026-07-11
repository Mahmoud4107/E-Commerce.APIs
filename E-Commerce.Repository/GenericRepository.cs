using E_Commerce.Core.Entities;
using E_Commerce.Core.RepostriesContruct;
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
            if(typeof(T) == typeof(Product))
                return (IEnumerable<T>) await _context.Products.Include(P => P.Brand).Include(P => P.Category).ToListAsync();

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Product))
                return await _context.Products.Where(P => P.Id ==id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;

            return await _context.Set<T>().FindAsync(id);
        }
    }
}
