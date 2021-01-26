using ApiRestImportador.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiRestImportador.Service.Api.Configuration
{
    public static class NativerDependencyInjectorSetup
    {
        public static void AddDependencyInjectorSetup(this IServiceCollection services)
        {
            #region Estoque

            NativeInjectorBootStrapper.RegisterServices(services);

            #endregion

            #region MediatR

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            #endregion           
        }
    }
}
