using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    namespace BloatedCustomer
    {
        public class Customer
        {
            public Customer(Guid id, AddressBook addresses, Orders orderHistory, PaymentDetails paymentDetails, LoyaltySummary loyalty)
            {
                this.Id = id;
                this.Addresses = addresses;
                this.OrderHistory = orderHistory;
                this.PaymentDetails = paymentDetails;
                this.Loyalty = loyalty;
            }

            public Guid Id { get; private set;}

            public AddressBook Addresses { get; private set; }

            public Orders OrderHistory { get; private set; }

            public PaymentDetails PaymentDetails { get; private set; }

            public LoyaltySummary Loyalty { get; private set; }
        }
    }

    namespace DistributionAwareCustomer
    {
        namespace MarketingBoundedContext
        {
            public class Loyalty
            {
                // ..

                public Guid CustomerId { get; private set; }

                public LoyaltySummary Loyalty { get; private set; }

                // ..
            }
        }

        namespace AccountsBoundedContext
        {
            public class CustomerOrderHistory
            {
                // ..

                public Guid CustomerId { get; private set; }

                public Orders Orders { get; private set; }

                // ..
            }
        }

        namespace BillingBoundedContext
        {
            public class PaymentDetails
            {
                public Guid CustomerId { get; private set; }

                public CardDetails Default { get; private set; }

                public CardDetails Alternate { get; private set; }

                // ..
            }
        }

    }

    // Classes below are bare minimum to support example of Customer above spanning multiple contexts

        public class AddressBook
        {
            public Address Home { get; private set; }

            public Address Work { get; private set; }

            // ..
        }

        public class Address {}

        public class Orders
        {
            private IEnumerable<Order> orders;

            public Orders(IEnumerable<Order> orders)
            {
                this.orders = orders;
            }

            // ..
        }

        public class Order
        {
            public Guid Id { get; private set; }

            // ..
        }

        public class PaymentDetails
        {
            public CardDetails DefaultPayment { get; private set; }

            public CardDetails AlternatePayment { get; private set; }
        }

        public class CardDetails
        {
            public Guid Id { get; private set;}

            public string Number { get; private set; }

            public string SecurityCode { get; private set; }
        }

        public class LoyaltySummary
        {
            public LoyaltyStatus Status { get; private set; }

            public int Points { get; private set; }
        }

        public enum LoyaltyStatus
        {
            Gold,
            Silver,
            Bronze
        }

}
