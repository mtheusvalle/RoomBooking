using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Application.Commands.Sala.AtualizarSala;

public class AtualizarSalaCommandHandler : IRequestHandler<AtualizarSalaCommand, SalaDto>
{
    private readonly ISalaRepository _salaRepository;
    private readonly IMapper _mapper;

    public AtualizarSalaCommandHandler(ISalaRepository salaRepository, IMapper mapper)
    {
        _salaRepository = salaRepository;
        _mapper = mapper;
    }

    public async Task<SalaDto> Handle(AtualizarSalaCommand request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.ObterPorIdAsync(request.Id);
        sala.AtualizarNome(request.NovoNome);

        var novaCapacidade = new Capacidade(request.NovaCapacidade);
        sala.AtualizarCapacidade(novaCapacidade);

        sala = new Domain.Entities.Sala(sala.Id, sala.Nome, sala.Capacidade);
        await _salaRepository.AtualizarAsync(sala);
        return _mapper.Map<SalaDto>(sala);
    }
}