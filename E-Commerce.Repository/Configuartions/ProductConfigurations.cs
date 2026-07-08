using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Configuartions
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(50);
            builder.Property(P => P.Description).IsRequired().HasMaxLength(50);
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.Property(P => P.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(P => P.CategoryId);
        }
    }
}
