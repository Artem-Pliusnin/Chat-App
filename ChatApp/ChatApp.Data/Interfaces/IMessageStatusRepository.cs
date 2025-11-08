using ChatApp.Data.Shared;
using ChatApp.Domain;

namespace ChatApp.Data.Interfaces;

public interface IMessageStatusRepository
{
    Task<OperationResult> CreateStatusesForMessage(Guid MessageId, IEnumerable<Guid> UsersIds);
}