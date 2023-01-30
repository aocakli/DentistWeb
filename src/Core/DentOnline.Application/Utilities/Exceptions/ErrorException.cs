namespace DentOnline.Application.Utilities.Exceptions;

public class ErrorException : Exception
{
    public ErrorException(string message) : base(message)
    {
    }
}