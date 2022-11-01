using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Entities
{
    public enum ShortUrlState
    {
        [Description("ایجاد شده")]
        Created = 1,
        [Description("لینک دارد")]
        ShortenPending = 2,
        [Description("لینک کوتاه دارد")]
        ReviewPending = 3,
        [Description("مشاهده شده")]
        Reviewed = 4
    }
}
