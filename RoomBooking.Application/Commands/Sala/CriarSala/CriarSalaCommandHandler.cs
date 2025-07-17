using MediatR;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Application.Commands.Sala.CriarSala;

public class CriarSalaCommandHandler : IRequestHandler<CriarSalaCommand, Guid>
{
    private readonly ISalaRepository _salaRepository;

    public CriarSalaCommandHandler(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task<Guid> Handle(CriarSalaCommand request, CancellationToken cancellationToken)
    {
        var capacidade = new Capacidade(request.Capacidade);
        var sala = new Domain.Entities.Sala(request.Id, request.Nome, capacidade);
        await _salaRepository.AdicionarAsync(sala);
        return sala.Id;
    }
}