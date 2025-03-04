namespace StoreBaeltTicketLibrary.Exception_classes;

/// <summary>
/// The exception that is thrown when a <c>DateTime</c> 
/// object is not of week day Saturday or Sunday.
/// </summary>
public class DateIsNotWeekendException : Exception
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public DateIsNotWeekendException() : base() { }

    public DateIsNotWeekendException(string message) : base(message) { }

    public DateIsNotWeekendException(string message, Exception inner) : base(message, inner) { }
}
