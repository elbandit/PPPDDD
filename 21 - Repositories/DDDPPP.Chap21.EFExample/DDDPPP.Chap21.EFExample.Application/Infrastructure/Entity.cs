using System;

namespace DDDPPP.Chap21.EFExample.Application.Infrastructure
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        public int Version { get; protected set; }
    }
}
