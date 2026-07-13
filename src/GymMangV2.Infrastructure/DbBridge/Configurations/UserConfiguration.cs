using GymMangV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMangV2.Infrastructure.DbBridge.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(x => x.UserName)
               .IsUnique();

        builder.Property(x => x.PassworHash)
               .IsRequired();

        builder.HasOne(x => x.Role)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Member)
               .WithOne(x => x.User)
               .HasForeignKey<Member>(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}