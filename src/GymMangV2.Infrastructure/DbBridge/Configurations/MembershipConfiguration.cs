using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;

public class MembershipConfiguration : IEntityTypeConfiguration<MemberShip>
{
    public void Configure(EntityTypeBuilder<MemberShip> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.StartingDate)
               .IsRequired();

        builder.Property(m => m.EndDate)
               .IsRequired();

        builder.HasOne(m => m.Member)
               .WithMany(mb => mb.MemberShips)
               .HasForeignKey(m => m.MemberID)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.MembershipPlan)
               .WithMany(mp => mp.Membership)
               .HasForeignKey(m => m.MemberShipPlanId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Payments)
               .WithOne(p => p.MemberShip)
               .HasForeignKey(p => p.MemberShipId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}