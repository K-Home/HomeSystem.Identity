using Autofac;
using HomeSystem.Services.Identity.Application.Mappings;

namespace HomeSystem.Services.Identity.Application.Modules
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.InitializeMapper());
        }
    }
}
