using Core.Domain.ShortUrl.Data;
using Core.Domain.ShortUrl.Dtoes;
using Core.Domain.ShortUrl.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SqlServer.ShortenUrl
{

    public class ShortUrlQueryService : IShortUrlQueryService
    {
        private readonly ShortUrlContext shortUrlContext;

        public ShortUrlQueryService(ShortUrlContext shortUrlContext)
        {
            this.shortUrlContext = shortUrlContext;
        }

        public ShortUrlDetails Query(GetShortUrl query)
        {
            return shortUrlContext.ShortUrls.Include(e => e.Reviews).Select(e => new ShortUrlDetails
            {
                Id = e.Id,
                UrlString = e.UrlString,
                ShortUrlString = e.ShortUrlString,
                Reviews = e.Reviews.ToList()
            }).FirstOrDefault(e => e.Id == query.ShortUrlId);
        }

        public ICollection<ShortUrlDetails> Query(GetShortUrlsList query)
        {
            return (ICollection<ShortUrlDetails>)shortUrlContext.ShortUrls.Include(e => e.Reviews)
                //.Where(e => e.Reviews.Where(d => d.ReviewDateTime >= query.From && d.ReviewDateTime <= query.To).ToList() != null)
                .Select(e => new ShortUrlDetails
                {
                    Id = e.Id,
                    UrlString = e.UrlString,
                    ShortUrlString = e.ShortUrlString,
                    Reviews = e.Reviews.ToList()
                })
            ;
        }
    }
}
