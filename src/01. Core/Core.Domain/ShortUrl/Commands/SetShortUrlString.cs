using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Commands
{
    public class SetShortUrlString
    {
        public Guid Id { get; set; }
        public string ShortUrlString { get; set; }
    }
}
