using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DDDPPP.Chap21.EFExample.Application.Infrastructure.DataModel;
using DDDPPP.Chap21.EFExample.Application.Infrastructure.Mapping;

namespace DDDPPP.Chap21.EFExample.Application.Infrastructure
{
    public partial class AuctionDatabaseContext : DbContext
    {
        static AuctionDatabaseContext()
        {
            Database.SetInitializer<AuctionDatabaseContext>(null);
        }

        public AuctionDatabaseContext()
            : base("Name=AuctionDatabaseContext")
        {
        }

        public DbSet<AuctionDTO> Auctions { get; set; }
        public DbSet<BidDTO> Bids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuctionMap());
            modelBuilder.Configurations.Add(new BidMap());           
        }

        public void Clear()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;

            var addedObjects = context
                             .ObjectStateManager
                             .GetObjectStateEntries(EntityState.Added);

            foreach (var objectStateEntry in addedObjects)
            {
                context.Detach(objectStateEntry.Entity);
            }

            var modifiedObjects = context
                             .ObjectStateManager
                             .GetObjectStateEntries(EntityState.Modified);

            foreach (var objectStateEntry in modifiedObjects)
            {
                context.Detach(objectStateEntry.Entity);
            }
        }
    }
}
