namespace Infrastructure.Exceptions
{
    public class BusinessException : Exception
    {
        public string? ErrorCode { get; }

        public BusinessException(string message, string errorCode)
            : base(message)
        { 
            ErrorCode = errorCode;
        }

        public BusinessException(string message) : base()
        { }

        public BusinessException(
            string message,
            Exception innerException)
            : base (message, innerException)
        { }
    }
}
