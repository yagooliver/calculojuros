using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using CalculaJuros.Domain.Shared.Notificacao;
using CalculaJuros.Domain.Shared.Handler;
using CalculaJuros.Domain.Core.Commands;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using CalculoJuros.CalculoApi.Extensions;

namespace CalculoJuros.CalculoApi.Api.Controllers
{
    public class CalculoJurosFunction : ApiBase
    {
        public CalculoJurosFunction(INotificationHandler<NotificacaoDominio> notificacoes, IMediatorHandler mediator) : base(notificacoes, mediator)
        {
        }

        [FunctionName("CalculoJurosFunction")]
        [OpenApiOperation("list", "Calculo API", Description = "Api para realizar calculo de juros compostos")]
        [OpenApiParameter("valorInicial",Summary = "Valor inicial", Description = "Valor inicial  para calculo", In = Microsoft.OpenApi.Models.ParameterLocation.Query)]
        [OpenApiParameter("meses", Summary = "Meses", Description = "Quantidade de meses", In = Microsoft.OpenApi.Models.ParameterLocation.Query)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculaJuros")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string valorInicial = req.Query["valorInicial"];
            string meses = req.Query["meses"];

            var command = new CalculaJurosCommand(decimal.Parse(valorInicial), int.Parse(meses));
            var resultado = await _mediator.SendCommandResult(command);
            var valor = resultado.ToStringDecimal();
            return Response(valor);
        }

        [FunctionName("ShowMeTheCodeFunction")]
        [Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes.OpenApiOperation("list", "Calculo API")]
        public IActionResult GetUrlGit(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "showmethecode")] HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult("https://github.com/yagooliver/calculojuros");
        }
    }
}
