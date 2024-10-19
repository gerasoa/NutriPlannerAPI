using CCRS.Api.Controllers;
using CCRS.Business.Interfaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/test")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier, 
                              IUser appUser) : base(notifier, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {

            //throw new Exception("Error - Test");

            //try
            //{
            //    var i = 0;
            //    var result = 42 / i;
            //}
            //catch (DivideByZeroException e)
            //{

            //    e.Ship(HttpContext);
            //}



            return "Sou a V1.1";
        }
    }
}
