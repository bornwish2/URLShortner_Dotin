using Core.Domain.ShortUrl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Data
{
    public interface IShortUrlRepository
    {
        bool Exists(Guid id);

        Entities.ShortUrl Load(Guid id);

        void Add(Entities.ShortUrl entity);
    }
}
