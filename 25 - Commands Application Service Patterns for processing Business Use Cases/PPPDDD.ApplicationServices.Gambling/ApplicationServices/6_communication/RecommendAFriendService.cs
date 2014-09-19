using System;
using Domain;
using NServiceBus;
using PPPDDD.ApplicationServices.Gambling.Messages;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.communication
{
    public class RecommendAFriendService
    {
        private ICustomerDirectory customerDirectory;
        private IReferAFriendPolicy referAFriendPolicy;
        private IBus bus;

        public RecommendAFriendService(ICustomerDirectory customerDirectory, IReferAFriendPolicy referAFriendPolicy,
            IBus bus)
        {
            this.customerDirectory = customerDirectory;
            this.referAFriendPolicy = referAFriendPolicy;
            this.bus = bus;
        }

        public void RecommendAFriend(int referrerId, NewAccount friendsAccountDetails)
        {
            Validate(friendsAccountDetails);

            using (var transaction = new System.Transactions.TransactionScope())
            {
                try
                {
                    var referrer = customerDirectory.Find(referrerId);
                    var friend = customerDirectory.Add(friendsAccountDetails);
                    referAFriendPolicy.Apply(referrer, friend);

                    transaction.Complete();

                    var msg = new CustomerRegisteredViaReferralPolicy
                    {
                        ReferrerId = referrerId,
                        FriendId = friend.Id
                    };
                    bus.Publish(msg);

                }
                catch (ReferralRejectedDueToLongTermOutstandingBalance)
                {
                    var msg = new ReferralSignupRejected
                    {
                        ReferrerId = referrerId,
                        FriendEmail = friendsAccountDetails.Email,
                        Reason = "Referrer has long term outstanding balance"
                    };
                    bus.Publish(msg);
                }
            }
        }

        private void Validate(NewAccount account)
        {
            // ...
        }
    }

    public class ReferralRejectedDueToLongTermOutstandingBalance : Exception { }
}

// keep messagees (external communication contract) in separate project (see chapter 12)
namespace PPPDDD.ApplicationServices.Gambling.Messages
{
    public class CustomerRegisteredViaReferralPolicy
    {
        public int ReferrerId { get; set; }

        public int FriendId { get; set; }
    }

    public class ReferralSignupRejected
    {
        public int ReferrerId { get; set; }

        public string FriendEmail { get; set; }

        public string Reason { get; set; }
    }
}