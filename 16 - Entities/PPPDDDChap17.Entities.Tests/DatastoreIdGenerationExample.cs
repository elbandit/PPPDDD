using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Mapping;
using PPPDDDChap17.Entities.Examples;
using System.IO;

namespace PPPDDDChap17.Entities.Tests
{
    [TestClass]
    public class DatastoreIdGenerationExample
    {
        ISession session = CreateSession();

        [TestMethod]
        public void Id_is_set_by_datastore_via_ORM()
        {
            var entity1 = new IdTestEntity();
            var entity2 = new IdTestEntity();

            // initially no id
            Assert.AreEqual(0, entity1.Id);
            Assert.AreEqual(0, entity2.Id);

            NHibernateTransaction(session =>
            {
                session.Save(entity1);
                session.Save(entity2);
            });

            // id will have been set via NHibernate
            Assert.AreEqual(1, entity1.Id);
            Assert.AreEqual(2, entity2.Id);
        }

        private void NHibernateTransaction(Action<ISession> action)
        {
            using (var transaction = session.BeginTransaction())
            {
                action(session);
                transaction.Commit();
            };
        }

        // See fluent NHibernate docs for more info: https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started
        private static ISession CreateSession()
        {
            Configuration configuration = null;

            // Manually configuring Map. NHibernate supports conventions and advanced features as well
            var factory = Fluently.Configure()
                           .Mappings(x => x.FluentMappings.Add(typeof(IdTestEntityMap)))
                           .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                           .ExposeConfiguration(cfg => configuration = cfg)
                           .BuildSessionFactory();

            var session = factory.OpenSession();

            BuildSchema(configuration, session);

            return session;
        }

        private static void BuildSchema(Configuration config, ISession session)
        {
            new SchemaExport(config).Execute(true, true, false, session.Connection, null);
        }
    }

    public class IdTestEntity
    {
        public virtual int Id { get; protected set; }
    }

    // Fluent NHibernate mapping class
    public class IdTestEntityMap : ClassMap<IdTestEntity>
    {
        public IdTestEntityMap()
        {
            Id(x => x.Id);
        }
    }
}
