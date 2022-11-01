using Core.ApplicationServices.ShortUrl.CommandHandler;
using Core.Domain.ShortUrl.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints.ShortUrlApi.Controllers
{
    [Route("Shortner")]
    public class ShortnerController : Controller
    {

        [HttpPost]
        public IActionResult Post([FromServices] SetUrlHandler handler, SetUrlString request)
        {
            handler.Handle(request);
            return Ok();
        }
    }
}
