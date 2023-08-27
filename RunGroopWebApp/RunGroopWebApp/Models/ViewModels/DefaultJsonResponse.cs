namespace RunGroopWebApp.Models.ViewModels
{
    public class DefaultJsonResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public DefaultJsonResponse(bool success, string? message)
        {
            Success = success;
            Message = message;
        }

        public DefaultJsonResponse() {}

    }
}
