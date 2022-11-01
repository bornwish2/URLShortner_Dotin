using Core.Domain.ShortUrl.Commands;
using Core.Domain.ShortUrl.Data;
using Core.Domain.ShortUrl.ValueObjects;
using Framework.ApplicationServices.CommandHandller;
using Framework.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ShortUrl.CommandHandler
{
    public class SetUrlHandler : ICommandHandler<SetUrlString>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShortUrlRepository shortUrlRepository;

        public SetUrlHandler(IUnitOfWork unitOfWork,IShortUrlRepository shortUrlRepository) 
        {
            this.unitOfWork = unitOfWork;
            this.shortUrlRepository = shortUrlRepository;
        }
        public void Handle(SetUrlString command)
        {
            if (shortUrlRepository.Exists(command.Id))
                throw new InvalidOperationException($"قبلا لینک با شناسه {command.Id} ثبت شده است.");

            var shortUrl = new Domain.ShortUrl.Entities.ShortUrl(command.Id);
            shortUrlRepository.Add(shortUrl);
            unitOfWork.Commit();
            shortUrl.SetUrlString(URL.FromString(command.UrlString));
            shortUrl.SetShortUrlString("http://localhost:5164/" + command.Id.ToString());
            unitOfWork.Commit();
        }
    }
}
