using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Interfaces;

namespace RoomBooking.Application.Queries.Reserva.ObterReservasPorSala;

public class ObterReservasPorSalaQueryHandler : IRequestHandler<ObterReservasPorSalaQuery, IEnumerable<ReservaDto>>
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IMapper _mapper;

    public ObterReservasPorSalaQueryHandler(IReservaRepository reservaRepository, IMapper mapper)
    {
        _reservaRepository = reservaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservaDto>> Handle(ObterReservasPorSalaQuery request, CancellationToken cancellationToken)
    {
        var reservas = await _reservaRepository.ObterPorSalaAsync(request.SalaId);
        return _mapper.Map<IEnumerable<ReservaDto>>(reservas);
    }
}