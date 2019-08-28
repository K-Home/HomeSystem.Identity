using AutoMapper;
using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Domain.ValueObjects;

namespace XSecure.Services.Users.Application.Mappings
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