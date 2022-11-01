using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Queries
{
    public class GetShortUrlsList
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
