using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Data.Configurations;

public class MessageStatusConfiguration: IEntityTypeConfiguration<MessageStatus>
{
    public void Configure(EntityTypeBuilder<MessageStatus> builder)
    {
        builder.HasKey(m=> m.Id);
        
        builder.Property(m=> m.UserId).IsRequired();

        builder.Property(m=> m.MessageId).IsRequired();

        builder.HasOne(m=> m.User).WithMany(u => u.MessageStatuses).HasForeignKey(m=>m.UserId);

        builder.HasOne(m=> m.Message).WithMany(m => m.MessageStatuses).HasForeignKey(m => m.MessageId);

        builder.Property(m => m.IsRead).HasDefaultValue(false);
    }
}