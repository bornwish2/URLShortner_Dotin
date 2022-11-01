using Framework.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Events
{
    public class ShortUrlReviewed : IEvent
    {
        public Guid Id { get; set; }
        public DateTime ReviewedAt { get; set; }
    }
}
