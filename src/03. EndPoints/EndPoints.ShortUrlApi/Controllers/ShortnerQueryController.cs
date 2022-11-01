using Core.ApplicationServices.ShortUrl.CommandHandler;
using Core.Domain.ShortUrl.Commands;
using Core.Domain.ShortUrl.Data;
using Core.Domain.ShortUrl.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints.ShortUrlApi.Controllers
{
    [Route("")]
    public class ShortnerQueryController : Controller
    {
        private readonly IShortUrlQueryService shortUrlQueryService;

        public ShortnerQueryController(IShortUrlQueryService shortUrlQueryService)
        {
            this.shortUrlQueryService = shortUrlQueryService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromServices] ReviewUrlHandler handler, string id)
        {
            var request = new GetShortUrl { ShortUrlId = new Guid(id) };
            
            var shortUrl= shortUrlQueryService.Query(request);
            handler.Handle(new ReviewShortUrl { Id = new Guid(id), ReviewedAt = DateTime.Now });

            return Redirect(shortUrl.UrlString);
        }

    }
}
