using Autofac;
using FinanceControl.Services.Users.Application.Mappings;

namespace FinanceControl.Services.Users.Application.Modules
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.InitializeMapper());
        }
    }
}