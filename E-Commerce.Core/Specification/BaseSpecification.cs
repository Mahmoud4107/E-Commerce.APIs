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
        public Expression<Func<T, bool>> Crateria { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Expression<Func<T, object>>> Includes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
