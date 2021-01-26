using ApiRestImportador.Domain.CommandHandlers;
using ApiRestImportador.Domain.Commands.Importacao;
using ApiRestImportador.Domain.Core.Bus;
using ApiRestImportador.Domain.Core.Notifications;
using ApiRestImportador.Domain.Interfaces.Repository;
using ApiRestImportador.Domain.Models;
using ApiRestImportador.Infra.CrossCutting.Bus;
using ApiRestImportador.Infra.Data.Context;
using ApiRestImportador.Infra.Data.Repository;
using ApiRestImportador.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRestImportador.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Domain Bus (Mediator)

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            #endregion

            #region Domain Core - Events

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #endregion

            #region Domain - commands

            // Importação
            services.AddScoped<IRequestHandler<NewImportacaoCommand, bool>, ImportacaoHandler>();

            // Layout
            services.AddScoped<LayoutPlanilhaExcel>();
            
            #endregion

            #region Infra - data

            services.AddScoped<IImportacaoRepository, ImportacaoRepository>();            
            services.AddScoped(typeof(ISelectSearchRepository<>), typeof(SelectSearchRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ImportadorContext>();

            #endregion          
        }
    }
}
