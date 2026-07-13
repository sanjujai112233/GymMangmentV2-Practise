using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.FullName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(t => t.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(t => t.Email)
               .HasMaxLength(150);

        builder.Property(t => t.Specialization)
               .HasMaxLength(100);

        builder.Property(t => t.Salary)
               .HasColumnType("decimal(18,2)");

        // One Trainer -> Many Members
        builder.HasMany(t => t.Members)
               .WithOne(m => m.Trainer)
               .HasForeignKey(m => m.TrainerId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}