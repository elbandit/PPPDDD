using System;

namespace DDDPPP.Chap21.MicroORM.Application.Infrastructure
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf(IAggregateDataModel entity);
        void PersistUpdateOf(IAggregateDataModel entity);
    }
}
