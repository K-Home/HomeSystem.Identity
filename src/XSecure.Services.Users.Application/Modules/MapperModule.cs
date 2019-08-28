using Autofac;
using XSecure.Services.Users.Application.Mappings;

namespace XSecure.Services.Users.Application.Modules
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.InitializeMapper());
        }
    }
}
