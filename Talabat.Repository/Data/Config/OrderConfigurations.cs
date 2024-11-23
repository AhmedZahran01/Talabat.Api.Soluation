using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());
            builder.Property(o => o.status)
                .HasConversion(
                ostatus => ostatus.ToString(),
                ostatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), ostatus)
                );

            builder.Property(o => o.SubTotal).
                HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.DeliveryMethod) .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
