namespace StoreBaeltTicketLibrary.Exception_classes;

public class DateIsNotWeekendException : Exception
{
    public DateIsNotWeekendException() : base() { }

    public DateIsNotWeekendException(string message) : base(message) { }

    public DateIsNotWeekendException(string message, Exception inner) : base(message, inner) { }
}
