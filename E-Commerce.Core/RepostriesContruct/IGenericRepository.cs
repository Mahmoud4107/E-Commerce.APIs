using E_Commerce.Core.Entities;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.RepostriesContruct
{
    public interface IGenericRepository <T> where T : ModelEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsyncWithSpec(ISpecification<T> specification);
        Task<T?> GetByIdAsyncWithSpec(ISpecification<T> specification);





    }
}
