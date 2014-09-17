using System;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}