using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Net6SignalR.Hubs;

namespace Net6SignalR.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly IHubContext<ServerMessageHub,IServerMessageHub> _hub;

    public HomeController(IHubContext<ServerMessageHub, IServerMessageHub> hub)
    {
        _hub = hub;
    }

    [HttpPost(Name = "ProcessFile")]
    public async Task<IActionResult> ProcessFile([FromForm] FileData data)
    {
        const string filePath = "./Files/";
        if (data.File is {Length: > 0})
        {
            _hub.Clients.Clients(data.ConnectionId).Receive(new() { Message = $"File received {data.File.FileName}" });
            _hub.Clients.Clients(data.ConnectionId).Receive(new() { Message = $"Processing..." });
            _hub.Clients.Clients(data.ConnectionId).Receive(new() { Message = $"Oh look", Type = ServerMessageType.Warning});
            _hub.Clients.Clients(data.ConnectionId).Receive(new() { Message = $"Oh no", Type = ServerMessageType.Error });
            Thread.Sleep(4000);
            await _hub.Clients.Clients(data.ConnectionId).Receive(new() { Message = $"Seems ok" });
        }

        await _hub.Clients.Clients(data.ConnectionId).Receive(new() { Message = $"done" });
        return Ok();
    }
}