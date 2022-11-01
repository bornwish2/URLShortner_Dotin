using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.ShortUrl.ValueObjects;

namespace Infrastructure.Data.SqlServer.ShortenUrl
{
    public class ShortUrlConfig : IEntityTypeConfiguration<Core.Domain.ShortUrl.Entities.ShortUrl>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.ShortUrl.Entities.ShortUrl> builder)
        {
            builder.Property(c => c.UrlString).HasConversion(c => c.Value, d => URL.FromString(d));
        }
    }
}
