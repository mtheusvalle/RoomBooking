namespace RoomBooking.Domain.ValueObjects;

public sealed class Capacidade
{
    public int Quantidade { get; }
    private const int MaxPermitido = 100;

    public Capacidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A capacidade deve ser maior que zero.", nameof(quantidade));

        if (quantidade > MaxPermitido)
            throw new ArgumentException($"A capacidade não pode exceder {MaxPermitido} pessoas.", nameof(quantidade));

        Quantidade = quantidade;
    }
}