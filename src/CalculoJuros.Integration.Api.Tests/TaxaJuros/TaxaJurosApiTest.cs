using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculoJuros.Integration.Api.Tests.TaxaJuros
{
    [TestClass]
    public class TaxaJurosApiTest
    {
        [TestMethod]
        public async Task Deve_retornar_taxa_de_juros()
        {             
            using var _http = new HttpClient();
            var response = await _http.GetAsync("https://taxajurosfunction.azurewebsites.net/api/taxaJuros");
            var resultado = 0.00;
            if (response.IsSuccessStatusCode)
            {
                resultado = Convert.ToDouble(await response.Content.ReadAsStringAsync(), System.Globalization.CultureInfo.InvariantCulture);
            }

            Assert.AreEqual(0.01, resultado);
        }
    }
}
