using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;
public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
               .HasMaxLength(150)
               .IsRequired();

        builder.HasOne(x => x.Trainer)
               .WithMany(x => x.Members)
               .HasForeignKey(x => x.TrainerId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}