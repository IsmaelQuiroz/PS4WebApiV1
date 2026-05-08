using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data.Configuration
{
    public class MonographConfiguration : IEntityTypeConfiguration<Monograph>
    {
        public void Configure(EntityTypeBuilder<Monograph> builder)
        {
            builder.Property(x => x.Keyword).HasMaxLength(200);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
           
            builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(X => X.CategoryId);

        }
    }
}
