using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Interfaces;

namespace RoomBooking.Application.Queries.Sala.ObterSalaPorId;

public class ObterSalaPorIdQueryHandler : IRequestHandler<ObterSalaPorIdQuery, SalaDto>
{
    private readonly ISalaRepository _salaRepository;
    private readonly IMapper _mapper;

    public ObterSalaPorIdQueryHandler(ISalaRepository salaRepository, IMapper mapper)
    {
        _salaRepository = salaRepository;
        _mapper = mapper;
    }

    public async Task<SalaDto> Handle(ObterSalaPorIdQuery request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.ObterPorIdAsync(request.Id);
        return _mapper.Map<SalaDto>(sala);
    }
}