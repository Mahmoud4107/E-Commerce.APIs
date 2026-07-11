using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public class StoreContextSeeding
    {
        public static async Task SeedAsync(StoreContext context)
        {
            // Brands Seeding
            var Branddata = await File.ReadAllTextAsync("../E-Commerce.Repository/Data/DataSeeding/brands.json");

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(Branddata);

            if(context.ProductBrands.Count() == 0)
            {
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await context.AddAsync(brand);
                } 
            await context.SaveChangesAsync();
            }

            
            // Categories Seeding 
            var Categorydata = await File.ReadAllTextAsync("../E-Commerce.Repository/Data/DataSeeding/categories.json");

            var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(Categorydata);

            if (context.ProductCategories.Count() == 0)
            {
                if (Categories?.Count > 0)
                {
                    foreach (var category in Categories)
                        await context.AddAsync(category);
                }
            await context.SaveChangesAsync();
            }


            // Product Seeding
            var Productdata = await File.ReadAllTextAsync("../E-Commerce.Repository/Data/DataSeeding/products.json");

            var Products = JsonSerializer.Deserialize<List<Product>>(Productdata);

            if (context.Products.Count() == 0)
            {
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                        await context.AddAsync(product);
                }
            await context.SaveChangesAsync();
            }

        }
    }
}
