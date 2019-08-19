using HomeSystem.Services.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(ua => ua.ZipCode)
                .HasMaxLength(18);

            builder.Property(ua => ua.Street)
                .HasMaxLength(180);

            builder.Property(ua => ua.State)
                .HasMaxLength(60);

            builder.Property(ua => ua.Country)
                .HasMaxLength(90);

            builder.Property(ua => ua.City)
                .HasMaxLength(100);
        }
    }
}
