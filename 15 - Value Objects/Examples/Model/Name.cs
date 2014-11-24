using Examples;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Examples
{
    public class Name : ValueObject<Name>
    {
        public readonly string firstName;
        public readonly string surname;

        public Name(string firstName, string surname)
        {
            Check.that(firstName.is_not_empty()).on_constraint_failure(() =>
            {
                throw new ApplicationException("You must specify a first name.");
            });

            Check.that(surname.is_not_empty()).on_constraint_failure(() =>
            {
                throw new ApplicationException("You must specify a surname.");
            });

            this.firstName = firstName;
            this.surname = surname;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { firstName, surname };
        }
    }

    public static class StringExtensions
    {
        public static bool is_not_empty(this String string_to_check)
        {
            return !String.IsNullOrEmpty(string_to_check);
        }
    }

    public class CheckConstraint
    {
        private readonly bool _assertion;

        public CheckConstraint(bool assertion)
        {
            _assertion = assertion;
        }

        public void on_constraint_failure(Action onFailure)
        {
            if (!_assertion) onFailure();
        }
    }

    public sealed class Check
    {
        public static CheckConstraint that(bool assertion)
        {
            return new CheckConstraint(assertion);
        }
    }
}

namespace NameWithPersistence
{
    public class Customer
    {
        public Customer()
        {
            // Required by NHibernate
        }

        public Customer(Guid id, Name name)
        {
            this.Id = id;
            this.Name = name;
        }

        // Virtual for NHibernate
        public virtual Guid Id { get; protected set; }

        public virtual Name Name { get; protected set; }
    }

    public class Name : ValueObject<Name>
    {
        public Name(string firstName, string surname)
        {
            this.FirstName = firstName;
            this.Surname = surname;
        }

        public string FirstName { get; protected set; }

        public string Surname { get; protected set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { FirstName, Surname };
        }

        public override string ToString()
        {
            return String.Format(
                "firstName:{0};;surname:{1}", FirstName, Surname
            );
        }
    }
 }

namespace NameWithPersistenceNormalized
{
    public class Customer
    {
        protected Customer()
        {
            // for NHibernate only
        }

        public Customer(Guid id, Name name)
        {
            this.Id = id;
            this.Name = name;
        }

        public virtual Guid Id { get; protected set; }

        public virtual Name Name { get; protected set; }

        // ...
    }

    public class Name : ValueObject<Name>
    {
        protected Name()
        {
            // Required by NHibernate
        }

        public Name(string firstName, string surname)
        {
            this.FirstName = firstName;
            this.Surname = surname;
        }

        public string FirstName { get; protected set; }

        public string Surname { get; protected set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { FirstName, Surname };
        }

    }
}
