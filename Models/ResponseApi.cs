namespace Game106.Backend.Models
{
    public class ResponseApi
    {
        public bool IsSuccess { get; set; } = true;
        public string Notification { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
