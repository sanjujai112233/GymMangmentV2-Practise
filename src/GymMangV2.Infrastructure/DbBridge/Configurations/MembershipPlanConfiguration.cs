using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;

public class MembershipPlanConfiguration : IEntityTypeConfiguration<MembershipPlan>
{
    public void Configure(EntityTypeBuilder<MembershipPlan> builder)
    {
        builder.ToTable("MembershipPlans");

        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(mp => mp.Name)
               .IsUnique();

        builder.Property(mp => mp.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(mp => mp.Description)
               .HasMaxLength(500);

        builder.HasMany(mp => mp.Membership)
               .WithOne(m => m.MembershipPlan)
               .HasForeignKey(m => m.MemberShipPlanId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}