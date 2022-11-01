using Core.Domain.ShortUrl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Dtoes
{
    public class ShortUrlDetails
    {
        public Guid Id { get; set; }
        public string UrlString { get; set; }
        public string ShortUrlString { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
