using AutoMapper;
using CarPriceAPI.Domains;
using CarPriceAPI.Models;

namespace CarPriceAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarModel, Car>();
            CreateMap<Car, CarHistoryModel>();
        }
    }
}
