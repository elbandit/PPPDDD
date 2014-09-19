using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AsyncAwaitDomain;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.AsyncAwait
{
    public class RecommendAFriendService
    {
        private ICustomerDirectory directory;
        private IReferAFriendPolicy policy;

        public RecommendAFriendService(ICustomerDirectory customerDirectory,
                                   IReferAFriendPolicy policy)
        {
            this.directory = customerDirectory;
            this.policy = policy;
        }

        public async void ReferAFriend(int referrerId, NewAccount friend)
        {
            // ...
            var referrer = await directory.Find(referrerId);
            var newAcct = await directory.Create(friend);
            policy.Apply(referrer, newAcct);
            // ...
        }
    }
}

namespace AsyncAwaitDomain
{
    public interface ICustomerDirectory
    {
        Task<Customer> Find(int customerId);

        Task<Customer> Create(NewAccount details);
    }

    public class NewAccount
    {
        public string Email { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
    }

    public interface IReferAFriendPolicy
    {
        void Apply(Customer referrer, Customer friend);
    }
}