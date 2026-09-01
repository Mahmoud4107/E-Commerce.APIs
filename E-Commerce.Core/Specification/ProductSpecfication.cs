using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class ProductSpecfication : BaseSpecification<Product>
    {
        public ProductSpecfication():base()
        {
            AddIncludes();
        }
        public ProductSpecfication(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes?.Add(P => P.Brand);
            Includes?.Add(P => P.Category);
        }

    }
}
