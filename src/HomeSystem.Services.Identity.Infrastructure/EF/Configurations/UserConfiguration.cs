using HomeSystem.Services.Identity.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(u => u.Username)
                .HasColumnName("Username")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar")
                .HasMaxLength(150)
                .IsRequired();           
            
            builder.Property(u => u.LastName)
                .HasColumnName("LastName")
                .HasColumnType("nvarchar")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnName("Role")
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(u => u.State)
                .HasColumnName("State")
                .HasColumnType("smallint")
                .IsRequired();
            
            builder.OwnsOne(u => u.Address);

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar")
                .HasMaxLength(12)
                .IsRequired();

            builder.Property(u => u.TwoFactorAuthentication)
                .HasColumnName("TwoFactorAuthentication")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("UpdatedAt");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.HasMany(u => u.UserSessions)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);
        }
    }
}