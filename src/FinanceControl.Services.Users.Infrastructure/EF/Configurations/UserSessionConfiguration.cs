using FinanceControl.Services.Users.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Services.Users.Infrastructure.EF.Configurations
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.HasKey(us => us.Id);

            builder.Property(us => us.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(us => us.Key)
                .HasColumnName("Key")
                .HasMaxLength(1000);

            builder.Property(us => us.UserAgent)
                .HasColumnName("UserAgent")
                .HasMaxLength(50);

            builder.Property(us => us.IpAddress)
                .HasColumnName("IpAddress")
                .HasMaxLength(50);

            builder.Property(us => us.ParentId)
                .HasColumnName("ParentId");

            builder.Property(us => us.Refreshed)
                .HasColumnName("Refreshed")
                .HasColumnType("bit");

            builder.Property(us => us.Destroyed)
                .HasColumnName("Destroyed")
                .HasColumnType("bit");

            builder.Property(us => us.UpdatedAt)
                .HasColumnName("UpdatedAt");

            builder.Property(us => us.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Ignore(us => us.DomainEvents);
        }
    }
}