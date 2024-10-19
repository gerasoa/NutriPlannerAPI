using CCRS.Api.Controllers;
using CCRS.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Api.V0.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/test")]
    public class TestController : MainController
    {
        private readonly ILogger _logger;

        public TestController(INotifier notifier,
                              IUser appUser,
                              ILogger<TestController> logger) : base(notifier, appUser)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Valor()
        {
            //Apenas em fase de desenvolvimento
            //_logger.LogTrace("Log de Trace");
            //_logger.LogDebug("Log de Debug");

            ////A ser utilizado a qualquer momento
            //_logger.LogWarning("Log de Informacao");
            //_logger.LogError("Log de Erroe");
            //_logger.LogCritical("Log de Problema Critico");




            return "Sou a V2.0";
        }
    }
}
