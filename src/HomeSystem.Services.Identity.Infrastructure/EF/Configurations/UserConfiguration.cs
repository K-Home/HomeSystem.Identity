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
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(150);

            builder.Property(u => u.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(150);

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("Password")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(u => u.Salt)
                .HasColumnName("Salt")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnName("Role")
                .IsRequired();

            builder.Property(u => u.State)
                .HasColumnName("State")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(12);

            builder.Property(u => u.TwoFactorAuthentication)
                .HasColumnName("TwoFactorAuthentication")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("UpdatedAt");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.OwnsOne(u => u.Address);

            builder.OwnsOne(u => u.Avatar);

            builder.HasMany(u => u.UserSessions)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            builder.HasMany(u => u.OneTimeSecuredOperations)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserEmail);

            builder.Ignore(u => u.DomainEvents);
        }
    }
}