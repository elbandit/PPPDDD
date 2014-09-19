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
        [TestMethod]
        public void Id_is_set_by_datastore_via_ORM()
        {
            var entity1 = new IdTestEntity();
            var entity2 = new IdTestEntity();

            // initially no Id
            Assert.AreEqual(0, entity1.Id);
            Assert.AreEqual(0, entity2.Id);

            NHibernateTransaction(session =>
            {
                session.Save(entity1);
                session.Save(entity2);
            });

            // Id will have been set via NHibernate
            Assert.AreEqual(1, entity1.Id);
            Assert.AreEqual(2, entity2.Id);
        }

        private void NHibernateTransaction(Action<ISession> action)
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                action(session);
                transaction.Commit();
            };
        }

        ISessionFactory sessionFactory = CreateSessionFactory();

        // See fluent NHibernate docs for more info: https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started
        private static ISessionFactory CreateSessionFactory()
        {

            var connString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + @"\entitiesdb.mdf;Integrated Security=True;Connect Timeout=30";

            // TODO: enable relative path
            //var connString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\John\Documents\entitiesdb.mdf;Integrated Security=True;Connect Timeout=30";
            
            // Manually configuring Map. NHibernate supports conventions and advanced features as well
            return Fluently.Configure()
                           .Mappings(x => x.FluentMappings.Add(typeof(IdTestEntityMap)))
                           .Database(
                                MsSqlConfiguration.MsSql2012.ConnectionString(connString)
                            )
                           .ExposeConfiguration(BuildSchema)
                           .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
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
