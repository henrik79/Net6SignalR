using Microsoft.AspNetCore.SignalR;

namespace Net6SignalR.Hubs;

public class ServerMessageHub : Hub<IServerMessageHub>
{
    public string GetConnectionId() => Context.ConnectionId;
}