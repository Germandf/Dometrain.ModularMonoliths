using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ComplexProperty(o => o.ShippingAddress, address =>
        {
            address.Property(a => a.Street1).HasMaxLength(DataSchemaConstants.STREET_MAX_LENGTH);
            address.Property(a => a.Street2).HasMaxLength(DataSchemaConstants.STREET_MAX_LENGTH);
            address.Property(a => a.City).HasMaxLength(DataSchemaConstants.CITY_MAX_LENGTH);
            address.Property(a => a.State).HasMaxLength(DataSchemaConstants.STATE_MAX_LENGTH);
            address.Property(a => a.PostalCode).HasMaxLength(DataSchemaConstants.POSTAL_CODE_MAX_LENGTH);
            address.Property(a => a.Country).HasMaxLength(DataSchemaConstants.COUNTRY_MAX_LENGTH);
        });
        builder.ComplexProperty(o => o.BillingAddress, address =>
        {
            address.Property(a => a.Street1).HasMaxLength(DataSchemaConstants.STREET_MAX_LENGTH);
            address.Property(a => a.Street2).HasMaxLength(DataSchemaConstants.STREET_MAX_LENGTH);
            address.Property(a => a.City).HasMaxLength(DataSchemaConstants.CITY_MAX_LENGTH);
            address.Property(a => a.State).HasMaxLength(DataSchemaConstants.STATE_MAX_LENGTH);
            address.Property(a => a.PostalCode).HasMaxLength(DataSchemaConstants.POSTAL_CODE_MAX_LENGTH);
            address.Property(a => a.Country).HasMaxLength(DataSchemaConstants.COUNTRY_MAX_LENGTH);
        });
    }
}
