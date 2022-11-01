using Core.Domain.ShortUrl.Dtoes;
using Core.Domain.ShortUrl.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Data
{
    public interface IShortUrlQueryService
    {
        ShortUrlDetails Query(GetShortUrl query);
        ICollection<ShortUrlDetails> Query(GetShortUrlsList query);
    }
}
