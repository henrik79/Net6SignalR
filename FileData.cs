namespace Net6SignalR
{
    public class FileData
    {
        public IFormFile? File { get; set; }
        public string ConnectionId { get; set; } = string.Empty;
    }
}
