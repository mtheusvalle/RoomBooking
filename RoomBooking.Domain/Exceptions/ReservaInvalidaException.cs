namespace RoomBooking.Domain.Exceptions;

public class ReservaInvalidaException : Exception
{
    public ReservaInvalidaException(string message)
        : base(message)
    {
    }
}