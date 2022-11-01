using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Commands
{
    public class SetUrlString
    {
        public Guid Id { get; set; }
        public string UrlString { get; set; }
    }
}
