namespace ProjectName.Contract.Error
{
    public class ErrorInfo // Common error info model
    {
        public int Code { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }
    }
}