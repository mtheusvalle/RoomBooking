using AutoMapper;
using MediatR;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Interfaces;

namespace RoomBooking.Application.Queries.Reserva.ObterReservaPorId;

public class ObterReservaPorIdQueryHandler : IRequestHandler<ObterReservaPorIdQuery, ReservaDto>
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IMapper _mapper;

    public ObterReservaPorIdQueryHandler(IReservaRepository reservaRepository, IMapper mapper)
    {
        _reservaRepository = reservaRepository;
        _mapper = mapper;
    }

    public async Task<ReservaDto> Handle(ObterReservaPorIdQuery request, CancellationToken cancellationToken)
    {
        var reserva = await _reservaRepository.ObterPorIdAsync(request.Id);
        return _mapper.Map<ReservaDto>(reserva);
    }
}