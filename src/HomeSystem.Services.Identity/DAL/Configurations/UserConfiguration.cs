using HomeSystem.Services.Identity.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeSystem.Services.Identity.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.AggregateId);

            builder.Property(u => u.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();           
            
            builder.Property(u => u.LastName)
                .HasColumnName("LastName")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnName("Role")
                .IsRequired();
            
            builder.OwnsOne(u => u.Address);

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("PhoneNumber")
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

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);

            builder.HasMany(u => u.UserSessions)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);
        }
    }
}