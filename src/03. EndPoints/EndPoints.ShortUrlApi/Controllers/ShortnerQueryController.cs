using Core.ApplicationServices.ShortUrl.CommandHandler;
using Core.Domain.ShortUrl.Commands;
using Core.Domain.ShortUrl.Data;
using Core.Domain.ShortUrl.Dtoes;
using Core.Domain.ShortUrl.Queries;
using Infrastructures.ApplicationServices.WebFramework.Api;
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


        [HttpGet("{id}")]
        public ActionResult Get([FromServices] ReviewUrlHandler handler, string id)
        {
            var request = new GetShortUrl { ShortUrlId = new Guid(id) };
            
            var shortUrl= shortUrlQueryService.Query(request);
            handler.Handle(new ReviewShortUrl { Id = new Guid(id), ReviewedAt = DateTime.Now });

            return Redirect(shortUrl.UrlString);
        }

        [HttpGet("report")]
        public ApiResult<ICollection<ShortUrlDetails>> Get()
        {
            var request = new GetShortUrlsList ();
            return Ok(shortUrlQueryService.Query(request));
        }

        [HttpGet("report/{id}")]
        public ApiResult<ShortUrlDetails> Get(string id)
        {
            var request = new GetShortUrl { ShortUrlId=new Guid(id)};
            return Ok(shortUrlQueryService.Query(request));
        }

    }
}
