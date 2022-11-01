using Core.Domain.ShortUrl.ValueObjects;
using Framework.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Events
{
    public class UrlStringChanged :IEvent
    {
        public Guid Id { get; set; }
        public string UrlString { get; set; }
    }
}
