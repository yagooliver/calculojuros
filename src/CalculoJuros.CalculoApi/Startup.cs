using CalculaJuros.CrossCutting.IOC;
using CalculoJuros.CalculoApi;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using MediatR;

[assembly: FunctionsStartup(typeof(Startup))]
namespace CalculoJuros.CalculoApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(Startup));
            builder.Services.RegisterServicesCalculo();
        }
    }
}
