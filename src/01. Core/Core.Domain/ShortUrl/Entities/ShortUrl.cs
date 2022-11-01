using Core.Domain.ShortUrl.Events;
using Core.Domain.ShortUrl.ValueObjects;
using Framework.Domain.Entieis;
using Framework.Domain.Events;
using Framework.Domain.Exceptions;
using Framework.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Domain.ShortUrl.Entities
{
    public class ShortUrl : BaseAggregateRoot<Guid>
    {

        #region Fields    
        public URL UrlString { get; protected set; }
        public string ShortUrlString { get; protected set; }
        public ShortUrlState State { get; protected set; }
        public ICollection<Review> Reviews { get; protected set; }

        #endregion

        private ShortUrl()
        {

        }

        /// <summary>
        /// short Url Created CTor
        /// </summary>
        /// <param name="id"></param>
        public ShortUrl(Guid id)
        {
            HandleEvent(new ShortUrlCreated
            {
                Id = id,
            });
        }

        /// <summary>
        /// set the user url string
        /// </summary>
        /// <param name="urlString">url string</param>
        public void SetUrlString(URL urlString)
        {
            HandleEvent(new UrlStringChanged
            {
                Id = Id,
                UrlString = urlString.Value
            });
        }

        /// <summary>
        /// set the shorten url string
        /// </summary>
        /// <param name="urlString">shorten url created from url</param>
        public void SetShortUrlString(string shortUrlString)
        {
            HandleEvent(new ShortUrlStringChanged
            {
                Id = Id,
                ShortUrlString = shortUrlString
            });
        }

        /// <summary>
        /// review the short url (Clicked)
        /// </summary>
        /// <param name="reviewedAt">clicked at (DateTime)</param>
        public void ReviewShortUrl(DateTime reviewedAt)
        {
            var newReview = new Review(HandleEvent);
            newReview.HandleEvent(new ShortUrlReviewed
            {
                Id = new Guid(),
                ReviewedAt = reviewedAt
            });
            Reviews.Add(newReview);
        }

        protected override void SetStateByEvent(IEvent @event)
        {
            switch (@event)
            {
                case ShortUrlCreated e:
                    Id = e.Id;
                    break;
                case UrlStringChanged e:
                    UrlString = URL.FromString(e.UrlString);
                    break;
                case ShortUrlStringChanged e:
                    ShortUrlString = e.ShortUrlString;
                    break;

                default:
                    throw new InvalidOperationException("امکان اجرای عملیات درخواستی وجود ندارد");
            }
        }

        protected override void ValidateInvariants()
        {
            //Impliment invariants

            var isValid =
               Id != null &&
               (State switch
               {
                   ShortUrlState.Created =>
                       UrlString == null
                       && ShortUrlString == null,

                   ShortUrlState.ShortenPending =>
                       UrlString != null
                       && ShortUrlString == null,

                   ShortUrlState.ReviewPending =>
                       UrlString != null
                       && ShortUrlString != null,

                   ShortUrlState.Reviewed =>
                        UrlString != null
                        && ShortUrlString != null
                        && Reviews == null,

                   _ => true
               });
            if (!isValid)
            {
                throw new InvalidEntityStateException(this, $"مقدار تنظیم شده برای آگهی در وضیعت {State.GetDescription()} غیر قابل قبول است");
            }
        }
    }
}
