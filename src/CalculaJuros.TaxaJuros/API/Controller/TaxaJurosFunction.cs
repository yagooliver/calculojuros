using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CalculaJuros.Domain.Core.Interface;
using CalculaJuros.Application.Extension;
using Microsoft.OpenApi.Models;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;

namespace CalculaJuros.TaxaJuros.API.Controller
{
    public class TaxaJurosFunction
    {
        private readonly ITaxaJurosService _taxaJurosService;

        public TaxaJurosFunction(ITaxaJurosService taxaJurosService)
        {
            _taxaJurosService = taxaJurosService;
        }

        [FunctionName("TaxaJurosFunction")]
        [OpenApiOperation("list", "Taxa Juros API")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "taxaJuros")] HttpRequest req,
            ILogger log)
        {
           return new OkObjectResult(await _taxaJurosService.GetTaxaJurosAsync());
        }
    }
}
