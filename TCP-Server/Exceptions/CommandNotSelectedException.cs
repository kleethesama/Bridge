namespace TCP_Server.Exceptions;

public class CommandNotSelectedException : Exception
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public CommandNotSelectedException() : base() { }

    public CommandNotSelectedException(string message) : base(message) { }

    public CommandNotSelectedException(string message, Exception inner) : base(message, inner) { }
}
