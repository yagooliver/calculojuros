using CalculaJuros.Domain.Shared.Retornos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculoJuros.Integration.Api.Tests.CalculoJuros
{
    [TestClass]
    public class CalculoJurosApiTest
    {
        [TestMethod]
        public async Task Deve_falhar_parametro_valor_inicial_invalido_ou_nao_informado()
        {
            using var _http = new HttpClient();
            var response = await _http.GetAsync($"https://calculojurosfunction.azurewebsites.net/api/calculaJuros?valorInicial=sda&meses=5");

            var resultado ="";
            if (!response.IsSuccessStatusCode)
            {
                resultado = await response.Content.ReadAsStringAsync();
            }
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual("Valor invalido", resultado);
        }

        [TestMethod]
        public async Task Deve_falhar_parametro_mes_invalido_ou_nao_informado()
        {
            using var _http = new HttpClient();
            var response = await _http.GetAsync($"https://calculojurosfunction.azurewebsites.net/api/calculaJuros?valorInicial=100&meses=fgas");

            var resultado = "";
            if (!response.IsSuccessStatusCode)
            {
                resultado = await response.Content.ReadAsStringAsync();
            }
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual("Mês invalido", resultado);
        }

        [TestMethod]
        public async Task Deve_falhar_parametro_valor_inicial_igual_zero()
        {
            using var _http = new HttpClient();
            var response = await _http.GetAsync($"https://calculojurosfunction.azurewebsites.net/api/calculaJuros?valorInicial=0&meses=5");

            var resultado = new ApiBadReturn();
            if (!response.IsSuccessStatusCode)
            {
                resultado = JsonConvert.DeserializeObject<ApiBadReturn>(await response.Content.ReadAsStringAsync());
            }
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual("Valor deve ser informado e não pode ser zero", resultado.errors.ToList()[0]);
        }

        [TestMethod]
        public async Task Deve_falhar_parametro_mes_igual_zero()
        {
            using var _http = new HttpClient();
            var response = await _http.GetAsync($"https://calculojurosfunction.azurewebsites.net/api/calculaJuros?valorInicial=10&meses=0");

            var resultado = new ApiBadReturn();
            if (!response.IsSuccessStatusCode)
            {
                resultado = JsonConvert.DeserializeObject<ApiBadReturn>(await response.Content.ReadAsStringAsync());
            }
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual("Quantidade de meses deve ser informado", resultado.errors.ToList()[0]);
        }
        [TestMethod]
        public async Task Deve_falhar_parametro_valor_inicial_e_mes_igual_zero()
        {
            using var _http = new HttpClient();
            var response = await _http.GetAsync($"https://calculojurosfunction.azurewebsites.net/api/calculaJuros?valorInicial=0&meses=0");

            var resultado = new ApiBadReturn();
            if (!response.IsSuccessStatusCode)
            {
                resultado = JsonConvert.DeserializeObject<ApiBadReturn>(await response.Content.ReadAsStringAsync());
            }
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual("Valor deve ser informado e não pode ser zero", resultado.errors.ToList()[0]);
            Assert.AreEqual("Quantidade de meses deve ser informado", resultado.errors.ToList()[1]);
        }
    }
}
