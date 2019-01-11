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

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}