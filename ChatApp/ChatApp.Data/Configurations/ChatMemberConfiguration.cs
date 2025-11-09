using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Data.Configurations;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.HasKey(c=> c.Id);
        
        builder.HasIndex(c => new { c.UserId, c.ChatId }).IsUnique();
        
        builder.Property(c=> c.ChatId).IsRequired();

        builder.Property(c=> c.UserId).IsRequired();

        builder.HasOne(c=> c.Chat)
            .WithMany(c => c.Members)
            .HasForeignKey(c=> c.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c=> c.User)
            .WithMany(u => u.Chats)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}