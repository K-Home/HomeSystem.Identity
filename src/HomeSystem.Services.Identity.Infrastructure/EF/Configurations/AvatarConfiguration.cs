using HomeSystem.Services.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Configurations
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(a => a.Url)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(a => a.IsEmpty)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
