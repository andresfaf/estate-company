namespace RealEstate.Application.Exceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message) : base(message)
        {
        }

        public DatabaseConnectionException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
