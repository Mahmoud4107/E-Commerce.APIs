using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public interface ISpecification<T> where T : ModelEntity
    {
        // Products.Where(P => P.Id ==id).Include(P => P.Brand).Include(P => P.Category)

        public Expression<Func<T, bool>> Crateria { get; set; }
        public List<Expression<Func<T,Object>>> Includes { get; set; }

        public Expression<Func<T,object>> OrderBy { get; set; } // orderby(P => P.Name)
        public Expression<Func<T,object>> OrderByDesc { get; set; } // orderbydesc(P => P.Name)

        public bool IsPagination { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

    }
}
