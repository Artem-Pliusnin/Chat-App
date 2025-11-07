using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(30);
        
        builder.Property(u => u.Password).IsRequired().HasMaxLength(300);
        
        builder.Property(u => u.Email).IsRequired().HasMaxLength(300);
        
        builder.HasIndex(u => u.UserName).IsUnique();
        
        builder.HasIndex(u => u.Email).IsUnique();
    }
}