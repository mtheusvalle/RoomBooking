namespace RoomBooking.Domain.ValueObjects;

public sealed class Periodo
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }

    public Periodo(DateTime inicio, DateTime fim)
    {
        if (fim <= inicio)
            throw new ArgumentException("O fim do período deve ser posterior ao início.", nameof(fim));

        Inicio = inicio;
        Fim = fim;
    }
}