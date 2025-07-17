using MediatR;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Application.Commands.Sala.AtualizarSala;

public class AtualizarSalaCommandHandler : IRequestHandler<AtualizarSalaCommand, Unit>
{
    private readonly ISalaRepository _salaRepository;

    public AtualizarSalaCommandHandler(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task<Unit> Handle(AtualizarSalaCommand request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.ObterPorIdAsync(request.Id);
        sala.AtualizarNome(request.NovoNome);
        // Capacidade não possui método de atualização, crie um novo VO
        sala = new Domain.Entities.Sala(sala.Id, sala.Nome, new Capacidade(request.NovaCapacidade));
        // Poderia ter método de atualização de capacidade na entidade
        await _salaRepository.AdicionarAsync(sala); // ou UpdateAsync
        return Unit.Value;
    }
}