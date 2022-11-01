using Core.Domain.ShortUrl.Events;
using Framework.Domain.Entieis;
using Framework.Domain.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.Entities
{
    public class Review : BaseEntity<Guid>
    {
        #region Fields
        public DateTime ReviewDateTime { get; private set; }

        //public FromIp Ip { get; private set; }
        //public FromOS OS { get; private set; }
        //public FromBrowser Browser { get; private set; }
        #endregion

        #region Constructors
        private Review()
        {

        }
        public Review(Action<IEvent> applier) : base(applier)
        {
        }
        #endregion

        protected override void SetStateByEvent(IEvent @event)
        {
            switch (@event)
            {
                case ShortUrlReviewed e:
                    Id = e.Id;
                    ReviewDateTime = e.ReviewedAt;
                    break;
            }
        }
    }
}
