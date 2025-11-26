using AutoMapper;
using Database.DTO.CarriageDTO;
using Database.DTO.JwtTokenDTO;
using Database.DTO.MainRouteDTO;
using Database.DTO.RegionDTO;
using Database.DTO.StationDTO;
using Database.DTO.SubRouteDTO;
using Database.DTO.TrainTypeDTO;
using Database.Models;

namespace Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Train, TrainsDTO>().ReverseMap();
            CreateMap<Train, UpdateTrainDTO>().ReverseMap();

            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();

            CreateMap<Township, TownshipDTO>().ReverseMap();
            CreateMap<Township, UpdateTownshipDTO>().ReverseMap();

            CreateMap<Carriage, CarriageDTO>().ReverseMap();
            CreateMap<Carriage, UpdateCarriageDTO>().ReverseMap();

            CreateMap<MainRoute, MainRouteDTO>().ReverseMap();
            CreateMap<MainRoute, UpdateMainRouteDTO>().ReverseMap();

            CreateMap<SubRoute, SubRouteDTO>().ReverseMap();

            CreateMap<Station, StationDTO>().ReverseMap();

            CreateMap<RefreshToken, RefreshTokenDTO>().ReverseMap();
        }
    }
}
