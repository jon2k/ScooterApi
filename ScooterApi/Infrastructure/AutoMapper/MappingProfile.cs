using AutoMapper;
using ScooterApi.Domain.Entities;
using ScooterApi.Models.v1;

namespace ScooterApi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataFromScooterModel, Scooter>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x=>x.Coordinate.X,opt=>opt.MapFrom(x=>x.Coordinate.X))
                .ForMember(x=>x.Coordinate.Y,opt=>opt.MapFrom(x=>x.Coordinate.Y))
                ;
           
        }
    }
}