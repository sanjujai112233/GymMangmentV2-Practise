using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("Attendances");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.CheckInTime)
               .IsRequired();

        builder.HasOne(a => a.Member)
               .WithMany(m => m.Attendences)
               .HasForeignKey(a => a.MemberID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}