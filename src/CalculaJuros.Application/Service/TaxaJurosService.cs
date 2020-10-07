using CalculaJuros.Domain.Core.Interface;

namespace CalculaJuros.Application.Service
{
    public class TaxaJurosService : ITaxaJurosService
    {
        public decimal GetTaxaJuros() => 0.01m;
    }
}
