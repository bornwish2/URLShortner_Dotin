using Autofac;
using Framework.ApplicationServices.Common;
using Framework.ApplicationServices.Data;
using Framework.Domain.Entieis;
using Infrastructure.Data.SqlServer;
using Infrastructure.Data.SqlServer.CoreConf;
using System;

namespace Infrastructures.ApplicationServices.WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //RegisterType > As > Liftetime
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var commonAssembly = typeof(SiteSettings).Assembly;
            //var entitiesAssembly = typeof(BaseEntity).Assembly;
            var aggregateAssembly = typeof(BaseAggregateRoot<>).Assembly;
            var dataAssembly = typeof(ShortUrlContext).Assembly;
            //var servicesAssembly = typeof(JwtService).Assembly;

            containerBuilder.RegisterAssemblyTypes(commonAssembly, dataAssembly, aggregateAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, dataAssembly, aggregateAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, dataAssembly, aggregateAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        //We don't need this since Autofac updates for ASP.NET Core 3.0+ Generic Hosting
        //public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        //{
        //    var containerBuilder = new ContainerBuilder();
        //    containerBuilder.Populate(services);
        //
        //    //Register Services to Autofac ContainerBuilder
        //    containerBuilder.AddServices();
        //
        //    var container = containerBuilder.Build();
        //    return new AutofacServiceProvider(container);
        //}
    }
}
