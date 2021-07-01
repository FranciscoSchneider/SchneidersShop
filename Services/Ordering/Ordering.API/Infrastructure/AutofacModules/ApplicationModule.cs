using Autofac;
using Ordering.Domain;
using Ordering.Infrastructure;

namespace Ordering.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderRepository>()
               .As<IOrderRepository>()
               .InstancePerLifetimeScope();
        }
    }
}
