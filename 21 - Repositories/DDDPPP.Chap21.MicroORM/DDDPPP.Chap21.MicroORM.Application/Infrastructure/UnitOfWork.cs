using System;
using System.Collections.Generic;
using System.Transactions;

namespace DDDPPP.Chap21.MicroORM.Application.Infrastructure
{
    public class UnitOfWork : IUnitOfWork 
    {
        private Dictionary<IAggregateDataModel, IUnitOfWorkRepository> addedEntities;
        private Dictionary<IAggregateDataModel, IUnitOfWorkRepository> changedEntities;

        public UnitOfWork()
        {
            addedEntities = new Dictionary<IAggregateDataModel, IUnitOfWorkRepository>();
            changedEntities = new Dictionary<IAggregateDataModel, IUnitOfWorkRepository>();
        }

        public void RegisterAmended(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository)
        {
            if (!changedEntities.ContainsKey(entity))
            {
                changedEntities.Add(entity, unitofWorkRepository);
            }
        }

        public void RegisterNew(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository)
        {
            if (!addedEntities.ContainsKey(entity))
            {
                addedEntities.Add(entity, unitofWorkRepository);
            };
        }

        public void Clear()
        {
            addedEntities.Clear();
            changedEntities.Clear();
        }
        
        public void Commit()
        {            
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (IAggregateDataModel entity in this.addedEntities.Keys)
                {
                    this.addedEntities[entity].PersistCreationOf(entity);
                }

                foreach (IAggregateDataModel entity in this.changedEntities.Keys)
                {
                    this.changedEntities[entity].PersistUpdateOf(entity);
                }

                scope.Complete();

                Clear();
            }
        }

    }
}
