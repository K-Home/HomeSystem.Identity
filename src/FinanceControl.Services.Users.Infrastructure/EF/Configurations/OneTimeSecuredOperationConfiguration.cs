using FinanceControl.Services.Users.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Services.Users.Infrastructure.EF.Configurations
{
    public class OneTimeSecuredOperationConfiguration : IEntityTypeConfiguration<OneTimeSecuredOperation>
    {
        public void Configure(EntityTypeBuilder<OneTimeSecuredOperation> builder)
        {
            builder.HasKey(otso => otso.Id);

            builder.Property(otso => otso.Type)
                .HasColumnName("Type")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(otso => otso.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(otso => otso.Token)
                .HasColumnName("Token")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(otso => otso.Expiry)
                .HasColumnName("Expiry")
                .IsRequired();
            
            builder.Property(otso => otso.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(otso => otso.RequesterIpAddress)
                .HasColumnName("RequesterIpAddress")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(otso => otso.RequesterUserAgent)
                .HasColumnName("RequesterUserAgent")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(otso => otso.ConsumerIpAddress)
                .HasColumnName("ConsumerIpAddress")
                .HasMaxLength(50);
            
            builder.Property(otso => otso.ConsumerUserAgent)
                .HasColumnName("ConsumerUserAgent")
                .HasMaxLength(50);
            
            builder.Property(otso => otso.ConsumedAt)
                .HasColumnName("ConsumedAt");

            builder.Property(otso => otso.Consumed)
                .HasColumnName("Consumed")
                .HasField("_consumed");

            builder.Ignore(otso => otso.DomainEvents);
        }
    }
}