namespace DDDPPP.Chap21.NHibernateExample.Application.Infrastructure
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; }
    }
}
