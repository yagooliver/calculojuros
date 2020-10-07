using CalculaJuros.TaxaJuros;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using CalculaJuros.CrossCutting.IOC;

[assembly: FunctionsStartup(typeof(Startup))]
namespace CalculaJuros.TaxaJuros
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.RegisterServices();
        }
    }
}
