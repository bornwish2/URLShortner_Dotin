using Core.Domain.ShortUrl.Commands;
using Core.Domain.ShortUrl.Data;
using Core.Domain.ShortUrl.Entities;
using Framework.ApplicationServices.CommandHandller;
using Framework.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ShortUrl.CommandHandler
{
    public class CreateHandler : ICommandHandler<Create>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShortUrlRepository shortUrlRepository;

        //private readonly IEventSource eventSource;

        public CreateHandler(IUnitOfWork unitOfWork,IShortUrlRepository shortUrlRepository)
        {
            this.unitOfWork = unitOfWork;
            this.shortUrlRepository = shortUrlRepository;
            //this.eventSource = eventSource;
        }
        public void Handle(Create command)
        {
            if (shortUrlRepository.Exists(command.Id))
                throw new InvalidOperationException($"قبلا لینک با شناسه {command.Id} ثبت شده است.");

            var shortUrl = new Domain.ShortUrl.Entities.ShortUrl(command.Id);
            shortUrlRepository.Add(shortUrl);
            unitOfWork.Commit();
            //var events = shortUrl.GetEvents();
            //eventSource.Save("ShortUrl", command.Id.ToString(), events);
        }
    }
}
