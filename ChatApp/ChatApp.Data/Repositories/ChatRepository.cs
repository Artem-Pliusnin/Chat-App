using ChatApp.Data.Interfaces;
using ChatApp.Data.Shared;
using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly ChatDbContext _context;

    public ChatRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<Chat?> GetByIdAsync(Guid id)
    {
        return await _context.Chats
            .Include(c => c.Members)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Chat>> GetByUserIDAsync(Guid userId)
    {
        return await _context.Chats
            .Include(c => c.Members)
            .Include(c => c.Messages).ThenInclude(m => m.MessageStatuses)
            .Where(c => c.Members.Any(m => m.UserId == userId))
            .OrderByDescending(c => c.Messages.Any()
                    ? c.Messages.Max(m => m.TimeStamp)
                    : c.TimeStamp
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<Chat>> GetAllAsync()
    {    
        return await _context.Chats
            .Include(c => c.Members)
            .ToListAsync();
    }

    public async Task<Chat> CreateAsync(Chat chat)
    {
        chat.Id = Guid.NewGuid();
        chat.TimeStamp = DateTime.UtcNow;

        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
        return chat;
    }

    public async Task<Chat?> UpdateAsync(Chat chat)
    {
        var existingChat = await _context.Chats.FindAsync(chat.Id);
        if (existingChat == null)
            return null;

        existingChat.Name = chat.Name;
        existingChat.TimeStamp = DateTime.UtcNow;

        _context.Chats.Update(existingChat);
        await _context.SaveChangesAsync();

        return existingChat;
    }

    public async Task<OperationResult> DeleteAsync(Guid id)
    {
        var chat = await _context.Chats.FindAsync(id);
        if (chat == null)
        {
            return OperationResult.Failure;
        }

        _context.Chats.Remove(chat);
        await _context.SaveChangesAsync();
        return OperationResult.Success;
    }
}
