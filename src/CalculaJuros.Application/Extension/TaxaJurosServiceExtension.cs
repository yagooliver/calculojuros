using CalculaJuros.Domain.Core.Interface;
using System.Threading.Tasks;

namespace CalculaJuros.Application.Extension
{
    public static class TaxaJurosServiceExtension
    {
        public static Task<decimal> GetTaxaJurosAsync(this ITaxaJurosService service) => Task.Run(() => service.GetTaxaJuros());
    }
}
