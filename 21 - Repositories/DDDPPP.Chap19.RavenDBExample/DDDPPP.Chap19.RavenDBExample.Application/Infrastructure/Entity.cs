using System;

namespace DDDPPP.Chap19.RavenDBExample.Application.Infrastructure
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; }
    }
}