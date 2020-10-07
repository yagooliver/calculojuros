using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalculaJuros.Domain.Shared.HttpHelper
{
    public interface IHttpClientCaller
    {
        Task<double> GetTaxaJuros(Dictionary<string, string> erros);
    }
}
