using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication;
using TestDomain;
using Rhino.Mocks;

namespace Tests
{
    [TestClass]
    public class Recommend_a_friend // class named after domain concept / use-case
    {
        private ReferAFriendService service;
        private IEmailer emailer;
        private ICustomerDirectory directory;

        // test data
        private int referrerId = 454456;
        private NewAccount friendsDetails = new NewAccount
        {
            Age = 24,
            Email = "pppddd@wrox.com",
            Nickname = "Deedeedee"
        };

        // this will run first (once only) then each test will run
        [ClassInitialize]
        public void When_a_user_signs_up_with_a_referral_from_a_friend()
        {
            // test as much of the implementation as possible
            directory = new CustomerDirectory(new InMemoryDatabase());
            var policy = new ReferralPolicy();
            
            // cannot test emailing implementation - easier to stub
            emailer = MockRepository.GenerateStub<IEmailer>();

            service = new ReferAFriendService(directory, policy, emailer);
            
            service.ReferAFriend(referrerId, friendsDetails);
        }

        [TestMethod]
        public void The_referrer_has_50_dollars_credited_to_their_account()
        {
            // ...
        }

        [TestMethod]
        public void The_friend_has_an_account_created_with_an_initial_50_dollars()
        {
            // ...
        }

        [TestMethod]
        public void The_referrers_loyalty_is_upgraded_to_gold_status()
        {
            var referrer = directory.Find(referrerId);
            Assert.AreEqual(LoyaltyStatus.Gold, referrer.LoyaltyStatus);
        }

        [TestMethod]
        public void The_refferer_gets_an_email_notifying_of_the_referral()
        {
            var referrer = directory.Find(referrerId);
            emailer.AssertWasCalled(em => 
            {
                em.SendReferralAcknowledgement(referrer);
            });
        }

        [TestMethod]
        public void The_friend_gets_an_email_notifying_of_account_creation()
        {
            // ...
        }
    }
}

// mimick an application
namespace TestApplication
{
    public class ReferAFriendService
    {
        private ICustomerDirectory directory;
        private IReferralPolicy policy;
        private IEmailer emailer;

        public ReferAFriendService(ICustomerDirectory directory, IReferralPolicy policy,
                                   IEmailer emailer)
        {
            this.directory = directory;
            this.policy = policy;
            this.emailer = emailer;
        }

        public void ReferAFriend(int referrerId, NewAccount friend)
        {
            // ***** Activity 1 - implement this if you want to make the tests pass
        }
    }

    public interface IEmailer
    {
        void SendReferralAcknowledgement(Customer customer);

        void SendPostReferralSignUpWelcome(Customer customer);
    }

    public class CustomerDirectory : ICustomerDirectory
    {
        public CustomerDirectory(InMemoryDatabase databaseContext)
        {
            // replace database context with a technology of your choice
        }

        public Customer Find(int customerId)
        {
            // ***** Activity 2 - implement this if you want to make the tests pass
            // You will need to choose an appropriate in-memory database
            return null;
        }
    }

    public class InMemoryDatabase
    {
        /*
         * Options for in-memory databases include:
         * - SQLite for use with SQL Server
         * - RavenDB embedded for RavenDB
         */
    }
}

// mimick a domain
namespace TestDomain
{
    public interface ICustomerDirectory
    {
        Customer Find(int customerId);
    }

    public interface IReferralPolicy
    {
        void Apply(Customer referrer, Customer friend);
    }

    public class ReferralPolicy : IReferralPolicy
    {
        public void Apply(Customer referrer, Customer friend)
        {
            // ***** Activity 3 - implement this if you want to make the tests pass
        }
    }

    public class Customer
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }

        public LoyaltyStatus LoyaltyStatus { get; set; }
    }

    public class NewAccount
    {
        public string Email { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }
    }

    public enum LoyaltyStatus
    {
        Bronze,
        Silver,
        Gold
    }
}

