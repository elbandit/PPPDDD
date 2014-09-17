namespace PPPDDDChap19.eBidder.Listings.Application.Infrastructure
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; }
    }
}
