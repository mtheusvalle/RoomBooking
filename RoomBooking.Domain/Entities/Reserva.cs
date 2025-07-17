using RoomBooking.Domain.Events;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Domain.Entities;

public class Reserva
{
    public Guid Id { get; private set; }
    public Guid SalaId { get; private set; }
    public Periodo Periodo { get; private set; }
    public string Organizador { get; private set; }

    private readonly IReservaConflictValidator _conflictValidator;

    public Reserva(Guid id, Guid salaId, Periodo periodo, string organizador, IReservaConflictValidator conflictValidator)
    {
        if (periodo == null)
            throw new ReservaInvalidaException("Período deve ser fornecido.");
        if (string.IsNullOrWhiteSpace(organizador))
            throw new ReservaInvalidaException("Organizador não pode ser vazio.");
        _conflictValidator = conflictValidator ?? throw new ReservaInvalidaException("Validador de conflito deve ser fornecido.");

        // Validar conflito de reservas
        if (_conflictValidator.ExisteConflitoAsync(salaId, periodo).GetAwaiter().GetResult())
            throw new ReservaInvalidaException("Já existe uma reserva conflitante para este período.");

        Id = id;
        SalaId = salaId;
        Periodo = periodo;
        Organizador = organizador;

        // Disparar evento de reserva criada (implementação de publicação fica em Application)
        var evento = new ReservaCriadaEvent(Id, SalaId, periodo.Inicio, periodo.Fim);
        // Aqui apenas para ilustrar; publicação real ocorre via MediatR ou Outbox
    }

    public void Cancelar()
    {
        // Lógica para cancelar a reserva (ex: marcar status)
    }
}