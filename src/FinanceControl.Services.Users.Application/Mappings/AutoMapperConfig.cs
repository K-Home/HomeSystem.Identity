using AutoMapper;
using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.ValueObjects;

namespace FinanceControl.Services.Users.Application.Mappings
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