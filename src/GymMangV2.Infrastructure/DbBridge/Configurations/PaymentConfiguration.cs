using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.PaymentDate)
               .IsRequired();

        builder.HasOne(p => p.MemberShip)
               .WithMany(m => m.Payments)
               .HasForeignKey(p => p.MemberShipId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}