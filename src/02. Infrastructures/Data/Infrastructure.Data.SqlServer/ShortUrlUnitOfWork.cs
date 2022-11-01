using Framework.Domain.Data;
using Framework.Domain.Entieis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SqlServer
{
    public class ShortUrlUnitOfWork : IUnitOfWork
    {
        private readonly ShortUrlContext shortUrlContext;
        private readonly IEventSource eventSource;

        public ShortUrlUnitOfWork(ShortUrlContext shortUrlContext, IEventSource eventSource)
        {
            this.shortUrlContext = shortUrlContext;
            this.eventSource = eventSource;
        }
        public int Commit()
        {
            var entityForSave = GetEntityForSave();
            int result = shortUrlContext.SaveChanges();
            SaveEvents(entityForSave);
            return result;
        }

        private void SaveEvents(List<EntityEntry> entityForSave)
        {
            foreach (var item in entityForSave)
            {
                var root = item.Entity as BaseAggregateRoot<Guid>;
                if (root != null)
                {
                    var id = root.Id.ToString();
                    var aggName = item.Entity.GetType().FullName;
                    eventSource.Save(aggName, id, root.GetEvents());
                }
            }
        }

        private List<EntityEntry> GetEntityForSave()
        {
            return shortUrlContext.ChangeTracker
              .Entries()
              .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted)
              .ToList();
        }
    }
}
