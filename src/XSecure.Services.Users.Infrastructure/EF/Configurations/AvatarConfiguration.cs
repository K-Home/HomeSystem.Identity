using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XSecure.Services.Users.Domain.ValueObjects;

namespace XSecure.Services.Users.Infrastructure.EF.Configurations
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(500);

            builder.Property(a => a.Url)
                .HasMaxLength(2000);

            builder.Property(a => a.IsEmpty);
        }
    }
}
