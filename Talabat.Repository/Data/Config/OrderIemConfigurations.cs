using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.Repository.Data.Config
{
    public class OrderIemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderitem => orderitem.product, product => product.WithOwner());
            builder.Property(orderitem => orderitem.Price) 
               .HasColumnType("decimal(18,2)");

        }
    }
}
