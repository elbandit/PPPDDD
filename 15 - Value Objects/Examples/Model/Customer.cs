using Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    // Entity
    public class Customer
    {
        public Customer(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }

        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }

        // ..
    }

    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        public readonly string Number;

        public PhoneNumber(string number)
        {
            this.Number = number;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { Number };
        }

        // ..
    }
}

namespace CustomerWithAddressBook
{
    // Entity
    public class Customer
    {
        public Customer(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }

        public PhoneBook PhoneNumbers { get; set; }

        // ..
    }

    public class PhoneBook : ValueObject<PhoneBook>
    {
        public readonly PhoneNumber HomeNumber;
        public readonly PhoneNumber MobileNumber;
        public readonly PhoneNumber WorkNumber;

        public PhoneBook(PhoneNumber homeNum, PhoneNumber mobileNum, PhoneNumber workNum)
        {
            this.HomeNumber = homeNum;
            this.MobileNumber = mobileNum;
            this.WorkNumber = workNum;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { HomeNumber, MobileNumber, WorkNumber };
        }
    }

    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        public readonly string Number;

        public PhoneNumber(string number)
        {
            this.Number = number;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { Number };
        }

        // ..
    }
}
