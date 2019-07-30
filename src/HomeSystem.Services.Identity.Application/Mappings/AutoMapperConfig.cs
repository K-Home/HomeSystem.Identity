using AutoMapper;
using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.ValueObjects;

namespace HomeSystem.Services.Identity.Application.Mappings
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserAddress, AddressDto>();
                cfg.CreateMap<User, BasicUserDto>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserSession, UserSessionDto>();
            });

            return config.CreateMapper();
        }
    }
}