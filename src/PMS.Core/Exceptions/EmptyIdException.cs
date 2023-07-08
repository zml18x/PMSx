namespace PMS.Core.Exceptions
{
    public class EmptyIdException : Exception
    {
        public EmptyIdException(string? message) : base(message) { }
    }
}