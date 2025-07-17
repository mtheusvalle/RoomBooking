using AutoMapper;
using RoomBooking.Application.DTOs;
using RoomBooking.Domain.Entities;

namespace RoomBooking.Application.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Sala, SalaDto>()
            .ForMember(dest => dest.Capacidade, opt => opt.MapFrom(src => src.Capacidade.Quantidade));

        CreateMap<Reserva, ReservaDto>()
            .ForMember(dest => dest.Inicio, opt => opt.MapFrom(src => src.Periodo.Inicio))
            .ForMember(dest => dest.Fim, opt => opt.MapFrom(src => src.Periodo.Fim));
    }
}