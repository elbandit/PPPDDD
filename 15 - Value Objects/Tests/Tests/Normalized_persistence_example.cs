using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameWithPersistenceNormalized;
namespace Tests
{
    [TestClass]
    public class Normalized_persistence_example
    {
        ISession session = CreateSession();
        
        [TestMethod]
        public void Persisting_normalized_value_objects()
        {
            Guid id = Guid.NewGuid();

            NHibernateTransaction(session =>
            {
                var name = new Name("Kevin", "Kingston");
                var customer = new Customer(id, name);
                session.Save(customer, id);
            });

            NHibernateTransaction(session =>
            {
                var customer = session.Get<Customer>(id);

                var name = new Name("Kevin", "Kingston");
                Assert.AreEqual(name, customer.Name);
            });
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
                           .Mappings(x => x.FluentMappings.Add(typeof(CustomerNormalizedMap)))
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
}

// Fluent NHibernate mapping class
public class CustomerNormalizedMap : ClassMap<Customer>
{
    public CustomerNormalizedMap()
    {
        Id(x => x.Id);

        // Create a separate table for the Value Object
        Join("CustomerName", join =>
        {
            join.KeyColumn("Id");
            join.Component(x => x.Name, c =>
            {
                c.Map(x => x.FirstName);
                c.Map(x => x.Surname);
            });
        });
    }
}


