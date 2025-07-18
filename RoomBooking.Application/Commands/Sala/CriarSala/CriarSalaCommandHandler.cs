using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Interfaces;
using RoomBooking.Domain.ValueObjects;

namespace RoomBooking.Application.Commands.Sala.CriarSala;

public class CriarSalaCommandHandler : IRequestHandler<CriarSalaCommand, SalaDto>
{
    private readonly ISalaRepository _salaRepository;
    private readonly IMapper _mapper;

    public CriarSalaCommandHandler(ISalaRepository salaRepository, IMapper mapper)
    {
        _salaRepository = salaRepository;
        _mapper = mapper;
    }

    public async Task<SalaDto> Handle(CriarSalaCommand request, CancellationToken cancellationToken)
    {
        var capacidade = new Capacidade(request.Capacidade);
        var sala = new Domain.Entities.Sala(Guid.NewGuid(), request.Nome, capacidade);
        await _salaRepository.AdicionarAsync(sala);
        return _mapper.Map<SalaDto>(sala);
    }
}