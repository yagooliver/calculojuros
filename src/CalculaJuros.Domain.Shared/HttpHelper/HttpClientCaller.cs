using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculaJuros.Domain.Shared.HttpHelper
{
    public class HttpClientCaller : IHttpClientCaller
    {
        private readonly string _url;

        public HttpClientCaller(string url)
        {
            _url = url ?? "https://taxajurosfunction.azurewebsites.net/api/taxaJuros?code=6mck77692Apk5EeqmtiL3rwtus6ruscwjULC1XtzuEiOiwDpN3T54A==";
        }

        public HttpClientCaller()
        {
            _url = "https://taxajurosfunction.azurewebsites.net/api/taxaJuros?code=6mck77692Apk5EeqmtiL3rwtus6ruscwjULC1XtzuEiOiwDpN3T54A==";
        }

        public async Task<double> GetTaxaJuros(Dictionary<string,string> erros)
        {
            try
            {
                using var _http = new HttpClient();
                var response = await _http.GetAsync(_url);
                var resultado = "0";
                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsStringAsync();
                }

                return Convert.ToDouble(resultado, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch(HttpRequestException)
            {
                erros.Add("HttpRequestException","Não foi possível consultar a taxa de juros");
            }

            return 0;
        }
    }
}
