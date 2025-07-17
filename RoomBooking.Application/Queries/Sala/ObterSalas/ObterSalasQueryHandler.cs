using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Interfaces;

namespace RoomBooking.Application.Queries.Sala.ObterSalas;

public class ObterSalasQueryHandler : IRequestHandler<ObterSalasQuery, IEnumerable<SalaDto>>
{
    private readonly ISalaRepository _salaRepository;
    private readonly IMapper _mapper;

    public ObterSalasQueryHandler(ISalaRepository salaRepository, IMapper mapper)
    {
        _salaRepository = salaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SalaDto>> Handle(ObterSalasQuery request, CancellationToken cancellationToken)
    {
        var salas = await _salaRepository.ListarTodasAsync();
        return _mapper.Map<IEnumerable<SalaDto>>(salas);
    }
}