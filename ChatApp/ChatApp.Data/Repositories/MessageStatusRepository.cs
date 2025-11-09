using ChatApp.Data.Interfaces;
using ChatApp.Data.Shared;
using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories;

public class MessageStatusRepository : IMessageStatusRepository
{
    private readonly ChatDbContext _context;

    public MessageStatusRepository(ChatDbContext context)
    {
        _context = context;
    }
    
    public async Task<OperationResult> CreateStatusesForMessage(Guid messageId, IEnumerable<Guid> userIds)
    {
        try
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message == null)
            {
                return OperationResult.Failure;
            }

            var statuses = userIds.Select(userId => new MessageStatus
            {
                MessageId = messageId,
                UserId = userId,
                IsRead = userId == message.SenderId
            }).ToList();

            await _context.MessageStatuses.AddRangeAsync(statuses);
            await _context.SaveChangesAsync();

            return OperationResult.Success;
        }
        catch (Exception ex)
        {
            var result = OperationResult.Failure;
            result.Message = $"Error creating message statuses: {ex.Message}";
            return result;
        }
    }

    public async Task<OperationResult> SetAsReadForUser(Guid messageId, Guid userId)
    {
        try{
            var status = await _context.MessageStatuses
                .FirstOrDefaultAsync(ms => ms.MessageId == messageId && ms.UserId == userId);
            status.IsRead = true;
            _context.MessageStatuses.Update(status);

            await _context.SaveChangesAsync();

            return OperationResult.Success;
        }
        catch (Exception ex)
        { 
            return OperationResult.Failure;
        }
    }
}