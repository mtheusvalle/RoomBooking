namespace RoomBooking.Domain.ValueObjects;

public sealed class Periodo
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }
    private static readonly TimeSpan MaxDuration = TimeSpan.FromHours(8);

    public Periodo(DateTime inicio, DateTime fim)
    {
        if (fim <= inicio)
            throw new ArgumentException("O fim do período deve ser posterior ao início.", nameof(fim));

        if (fim - inicio > MaxDuration)
            throw new ArgumentException($"O período não pode exceder {MaxDuration.TotalHours} horas.", nameof(fim));

        Inicio = inicio;
        Fim = fim;
    }
}