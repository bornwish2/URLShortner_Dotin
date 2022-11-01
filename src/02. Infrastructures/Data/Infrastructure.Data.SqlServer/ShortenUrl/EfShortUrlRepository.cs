using Core.Domain.ShortUrl.Data;
using Core.Domain.ShortUrl.Entities;
using Framework.ApplicationServices.Data;
using Infrastructure.Data.SqlServer.CoreConf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SqlServer.ShortenUrl
{
    public class EfShortUrlRepository : Repository<Core.Domain.ShortUrl.Entities.ShortUrl>, IShortUrlRepository, IScopedDependency
    {
        private readonly ShortUrlContext shortUrlContext;

        public EfShortUrlRepository(ShortUrlContext shortUrlContext)
            : base(shortUrlContext)
        {
            this.shortUrlContext = shortUrlContext;
        }
        public void Add(Core.Domain.ShortUrl.Entities.ShortUrl entity)
        {
            shortUrlContext.ShortUrls.Add(entity);
        }

        public void Dispose()
        {
            shortUrlContext.Dispose();
        }

        public bool Exists(Guid id)
        {
            return shortUrlContext.ShortUrls.Any(c => c.Id == id);
        }

        public ShortUrl Load(Guid id)
        {
            return shortUrlContext.ShortUrls.Find(id);
        }
    }
}
