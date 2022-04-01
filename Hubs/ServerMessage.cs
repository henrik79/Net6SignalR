namespace Net6SignalR.Hubs;

public class ServerMessage
{
    public ServerMessageType Type { get; set; } = ServerMessageType.Information;
    public DateTime Time { get; set; } = DateTime.Now;
    public string Message { get; set; } = string.Empty;
}

public enum ServerMessageType
{
    Information,
    Warning,
    Error
}