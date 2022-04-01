namespace Net6SignalR.Hubs;

public interface IServerMessageHub
{
    Task Receive(ServerMessage message);
}