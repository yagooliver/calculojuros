using CalculaJuros.Domain.Core.Commands;
using CalculaJuros.Domain.Core.Handler;
using CalculaJuros.Domain.Shared.Handler;
using CalculaJuros.Domain.Shared.HttpHelper;
using CalculaJuros.Domain.Shared.Notificacao;
using CalculaJuros.TaxaJuros;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CalculaJuros.Domain.Test.CommandHandler
{
    [TestClass]
    public class CalculoJuroCommandHandlerTest
    {
        private NotificacaoDominioHandler _notificacaoDominioHandler;
        private Mock<IMediatorHandler> _mockMediator;
        private CalculaJurosCommandHandler _calculaJurosCommandHandler;
        private Mock<IHttpClientCaller> _mockHttpClient;

        [TestInitialize]
        public void Inicializa()
        {
            _mockMediator = new Mock<IMediatorHandler>();
            _notificacaoDominioHandler = new NotificacaoDominioHandler();

            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<NotificacaoDominio>())).Callback<NotificacaoDominio>((x) =>
            {
                _notificacaoDominioHandler.Handle(x, CancellationToken.None);
            });
            _mockHttpClient = new Mock<IHttpClientCaller>();

            _mockHttpClient.Setup(x => x.GetTaxaJuros(It.IsAny<Dictionary<string,string>>())).Returns(Task.FromResult<double>(0.01));
        }

        [TestMethod]
        public async Task Deve_falhar_ao_calcular_servico_taxa_juros_offline()
        {
            var command = new CalculaJurosCommand(100m, 5);
            _calculaJurosCommandHandler = new CalculaJurosCommandHandler(_mockMediator.Object, new HttpClientCaller("http://localhost:7071/api/taxaJuros"));
            var resultado = await _calculaJurosCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(resultado == 0m);
            Assert.IsTrue(_notificacaoDominioHandler.HasNotifications());
            Assert.AreEqual("Não foi possível consultar a taxa de juros", _notificacaoDominioHandler.GetNotificacoes().FirstOrDefault(x => x.Chave == "HttpRequestException").Valor);
        }

        [TestMethod]
        public async Task Deve_falhar_validacao_valor_zerado()
        {
            var command = new CalculaJurosCommand(0, 5);

            _calculaJurosCommandHandler = new CalculaJurosCommandHandler(_mockMediator.Object, new HttpClientCaller("http://localhost:7071/api/taxaJuros"));
            var resultado = await _calculaJurosCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(resultado == 0m);
            Assert.IsTrue(_notificacaoDominioHandler.HasNotifications());
            Assert.AreEqual("Valor deve ser informado e não pode ser zero", _notificacaoDominioHandler.GetNotificacoes()[0].Valor);
        }

        [TestMethod]
        public async Task Deve_falhar_validacao_meses_zerado()
        {
            var command = new CalculaJurosCommand(100m, 0);

            _calculaJurosCommandHandler = new CalculaJurosCommandHandler(_mockMediator.Object, new HttpClientCaller("http://localhost:7071/api/taxaJuros"));
            var resultado = await _calculaJurosCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(resultado == 0m);
            Assert.IsTrue(_notificacaoDominioHandler.HasNotifications());
            Assert.AreEqual("Quantidade de meses deve ser informado", _notificacaoDominioHandler.GetNotificacoes()[0].Valor);
        }

        [TestMethod]
        public async Task Deve_falhar_validacao_valor_meses_invalido()
        {
            var command = new CalculaJurosCommand(0, 0);

            _calculaJurosCommandHandler = new CalculaJurosCommandHandler(_mockMediator.Object, new HttpClientCaller("http://localhost:7071/api/taxaJuros"));
            var resultado = await _calculaJurosCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(resultado == 0m);
            Assert.IsTrue(_notificacaoDominioHandler.HasNotifications());
            Assert.IsTrue(_notificacaoDominioHandler.GetNotificacoes().Count == 2);
            Assert.AreEqual("Valor deve ser informado e não pode ser zero", _notificacaoDominioHandler.GetNotificacoes()[0].Valor);
            Assert.AreEqual("Quantidade de meses deve ser informado", _notificacaoDominioHandler.GetNotificacoes()[1].Valor);
        }

        [TestMethod]
        public async Task Deve_realizar_calculo()
        {
            var command = new CalculaJurosCommand(100, 5);

            _calculaJurosCommandHandler = new CalculaJurosCommandHandler(_mockMediator.Object, _mockHttpClient.Object);
            var resultado = await _calculaJurosCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(resultado == 105.1m);
            Assert.IsFalse(_notificacaoDominioHandler.HasNotifications());
        }

    }
}
