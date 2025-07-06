using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Infrastructure.Data;

internal class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable(nameof(UserAddress));
        builder.ComplexProperty(x => x.Address);
    }
}
