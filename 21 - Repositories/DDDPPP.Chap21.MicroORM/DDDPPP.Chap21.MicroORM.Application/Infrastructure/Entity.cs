using System;
using System.Collections.Generic;

namespace DDDPPP.Chap21.MicroORM.Application.Infrastructure
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        public int Version { get; protected set; }
    }
}
