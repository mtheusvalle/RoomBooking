using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Domain.Entities;

public class Sala
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Capacidade Capacidade { get; private set; }

    private Sala() { }

    public Sala(Guid id, string nome, Capacidade capacidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new SalaInvalidaException("O nome da sala não pode ser vazio.");

        Id = id;
        Nome = nome;
        Capacidade = capacidade ?? throw new SalaInvalidaException("Capacidade deve ser fornecida.");
    }

    public void AtualizarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new SalaInvalidaException("O nome da sala não pode ser vazio.");

        Nome = novoNome;
    }

    public void AtualizarCapacidade(Capacidade novaCapacidade)
    {
        if (novaCapacidade == null)
            throw new SalaInvalidaException("Capacidade deve ser fornecida.");
        Capacidade = novaCapacidade;
    }
}