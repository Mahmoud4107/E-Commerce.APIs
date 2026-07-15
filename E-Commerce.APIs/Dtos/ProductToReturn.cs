using E_Commerce.Core.Entities;

namespace E_Commerce.APIs.Dtos
{
    public class ProductToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int BrandId { get; set; } 
        public string Brand { get; set; } 
        public int CategoryId { get; set; } 
        public string Category { get; set; }
    }
}
