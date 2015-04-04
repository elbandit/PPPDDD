using System;

namespace DDDPPP.Chap21.MicroORM.Application.Infrastructure
{
    public interface IUnitOfWork
    {
        void RegisterAmended(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterNew(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository);
        void Commit();
        void Clear();
    }
}
