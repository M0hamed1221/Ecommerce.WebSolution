using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(product => product.ProductBrand)
                 .WithMany()
                 .HasForeignKey(product => product.BrandId);

            builder.HasOne(product => product.ProductType)
               .WithMany()
               .HasForeignKey(product => product.TypeId);

            builder.Property(Prod => Prod.Price)
                   .HasColumnType("decimal(10,3)");

        }
    }
}
