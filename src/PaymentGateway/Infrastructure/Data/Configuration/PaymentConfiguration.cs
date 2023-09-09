using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Infrastructure.Data.Entities;

namespace PaymentGateway.Infrastructure.Data.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments").HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Ignore(p => p.Events);
    }
}
