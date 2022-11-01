using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Commands
{
    public class ReviewShortUrl
    {
        public Guid Id { get; set; }
        public DateTime ReviewedAt { get; set; }
    }
}
