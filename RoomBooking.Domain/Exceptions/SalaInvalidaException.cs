namespace RoomBooking.Domain.Exceptions;

public class SalaInvalidaException : Exception
{
    public SalaInvalidaException(string message)
        : base(message)
    {
    }
}