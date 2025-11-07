using System.Reflection;
using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;

public class ChatDbContext: DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<MessageStatus> MessageStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}