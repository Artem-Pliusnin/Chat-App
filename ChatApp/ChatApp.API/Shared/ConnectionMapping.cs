namespace ChatApp.API.Shared;

public static class ConnectionMapping
{
    private static readonly Dictionary<Guid, List<string>> _connections = new();

    public static void Add(Guid userId, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(userId, out var connections))
            {
                connections = new List<string>();
                _connections[userId] = connections;
            }

            connections.Add(connectionId);
        }
    }

    public static void Remove(Guid userId, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(userId, out var connections)) return;

            connections.Remove(connectionId);
            if (connections.Count == 0)
            {
                _connections.Remove(userId);
            }
        }
    }

    public static List<string> GetConnections(Guid userId)
    {
        lock (_connections)
        {
            return _connections.TryGetValue(userId, out var connections)
                ? new List<string>(connections)
                : new List<string>();
        }
    }
}
