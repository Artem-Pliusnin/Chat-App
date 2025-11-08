using ChatApp.Data.Interfaces;
using ChatApp.Data.Shared;
using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories;

public class MessageRepository : IMessageRepository
{
     private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> GetByIdAsync(Guid id)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Chat)
                .Include(m => m.MessageStatuses)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Message>> GetByChatIDAsync(Guid chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.TimeStamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Chat)
                .ToListAsync();
        }

        public async Task<Message> CreateAsync(Message message)
        {
            message.TimeStamp = DateTime.UtcNow;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message?> UpdateAsync(Message message)
        {
            var existing = await _context.Messages.FindAsync(message.Id);
            if (existing == null)
            {
                return null;
            }

            existing.Text = message.Text;
            existing.TimeStamp = DateTime.UtcNow;

            _context.Messages.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return OperationResult.Failure;
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return OperationResult.Success;
        }
}