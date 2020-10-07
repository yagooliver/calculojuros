using CalculaJuros.Application.Service;
using CalculaJuros.CrossCutting.Bus;
using CalculaJuros.Domain.Core.Commands;
using CalculaJuros.Domain.Core.Handler;
using CalculaJuros.Domain.Core.Interface;
using CalculaJuros.Domain.Shared.Handler;
using CalculaJuros.Domain.Shared.HttpHelper;
using CalculaJuros.Domain.Shared.Notificacao;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CalculaJuros.CrossCutting.IOC
{
    public static class DependencyInjectionResolver
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Service
            services.AddScoped<ITaxaJurosService, TaxaJurosService>();
        }

        public static void RegisterServicesCalculo(this IServiceCollection services)
        {
            //MediatR
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Commands
            services.AddScoped<IRequestHandler<CalculaJurosCommand, decimal>, CalculaJurosCommandHandler>();

            //shared
            services.AddScoped<IHttpClientCaller, HttpClientCaller>();

            //Domain notification
            services.AddScoped<INotificationHandler<NotificacaoDominio>, NotificacaoDominioHandler>();
        }
    }
}
