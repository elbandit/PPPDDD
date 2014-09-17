using System;

namespace DDDPPP.Chap19.MicroORM.Application.Infrastructure
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf(IAggregateDataModel entity);
        void PersistUpdateOf(IAggregateDataModel entity);
    }
}
