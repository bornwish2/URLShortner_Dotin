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
            var ShortUrl = shortUrlRepository.Load(command.Id);
            if (ShortUrl == null)
                throw new InvalidOperationException($"لینک با شناسه {command.Id} یافت نشد.");
            ShortUrl.SetUrlString(URL.FromString(command.UrlString));
            unitOfWork.Commit();
        }
    }
}
