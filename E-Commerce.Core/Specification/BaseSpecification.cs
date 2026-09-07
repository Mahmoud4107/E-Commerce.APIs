using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : ModelEntity
    {
        public Expression<Func<T, bool>> Crateria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public bool IsPagination { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> _Crateria)
        {
            Crateria = _Crateria;
        }

        public void ApplyPagination(int  skip, int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }
    }
}
