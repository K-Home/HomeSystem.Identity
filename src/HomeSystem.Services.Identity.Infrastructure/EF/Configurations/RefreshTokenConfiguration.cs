using HomeSystem.Services.Identity.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(rt => rt.Token)
                .HasColumnName("Token")
                .IsRequired();

            builder.Property(rt => rt.Revoked)
                .HasColumnName("Revoked");
        }
    }
}