using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
    public class Product : ModelEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int BrandId { get; set; } // Foreign Key
        public ProductBrand Brand { get; set; } // Navigational Property [ONE]
        public int CategoryId { get; set; } // Foreigen Key
        public ProductCategory Category { get; set; } // Navigational Property [ONE]
    }
}
