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
        public ProductSpecfication(ProductSpecParams productSpec):base( P =>
                                  
                                  ((string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search)) &&
                                   (!productSpec.BrandId.HasValue || P.BrandId == productSpec.BrandId) &&
                                    (!productSpec.CategoryId.HasValue || P.CategoryId == productSpec.CategoryId)))
        {
            AddIncludes();

            if(!string.IsNullOrEmpty(productSpec.Sort))
            {
                if(productSpec.Sort == "priceAsc")
                    OrderBy = P => P.Price;
                else if(productSpec.Sort == "priceDesc")
                    OrderByDesc = P => P.Price;
                else
                    OrderBy = P => P.Name;
            }
            else
                OrderBy = P => P.Name;

            ApplyPagination((productSpec.PageIndex - 1) * productSpec.Pagesize, productSpec.Pagesize);
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
