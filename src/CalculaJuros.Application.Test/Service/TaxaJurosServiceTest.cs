using CalculaJuros.Application.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CalculaJuros.Application.Test.Service
{
    [TestClass]
    public class TaxaJurosServiceTest
    {
        [TestMethod]
        public async Task Deve_retorna_taxa_juros_corretamente()
        {
            var esperado = 0.01m;
            var instancia = new TaxaJurosService();
            var resultado = await Task.Run(() => instancia.GetTaxaJuros());

            Assert.AreEqual(esperado, resultado);
        }
    }
}
