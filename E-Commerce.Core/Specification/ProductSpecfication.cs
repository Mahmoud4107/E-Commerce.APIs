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
        public ProductSpecfication(string sort):base()
        {
            AddIncludes();

            if(!string.IsNullOrEmpty(sort))
            {
                if(sort == "priceAsc")
                    OrderBy = P => P.Price;
                else if(sort == "priceDesc")
                    OrderByDesc = P => P.Price;
                else
                    OrderBy = P => P.Name;
            }
            else
                OrderBy = P => P.Name;
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
