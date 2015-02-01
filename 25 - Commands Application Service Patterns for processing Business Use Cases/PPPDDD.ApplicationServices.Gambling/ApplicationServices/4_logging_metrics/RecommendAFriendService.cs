using System;
using StatsdClient;
using Domain;
using log4net;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.LoggingAndMetrics
{
    public class RecommendAFriendService
    {
        private ILog logger = LogManager.GetLogger(typeof(RecommendAFriendService));
        private ICustomerDirectory customerDirectory;
        private IReferAFriendPolicy referAFriendPolicy;

        public RecommendAFriendService(ICustomerDirectory customerDirectory, IReferAFriendPolicy referAFriendPolicy)
        {
            this.customerDirectory = customerDirectory;
            this.referAFriendPolicy = referAFriendPolicy;
        }

        public void RecommendAFriend(int referrerId, NewAccount friendsAccountDetails)
        {
            Validate(friendsAccountDetails);

            // most technologies have similar transaction APIs
            using (var transaction = new System.Transactions.TransactionScope())
            {
                try
                {
                    // customerDirectory is a domain repository
                    var referrer = customerDirectory.Find(referrerId);
                    var friend = customerDirectory.Add(friendsAccountDetails);
                    // referAFriend policy is a domain policy
                    referAFriendPolicy.Apply(referrer, friend);

                    transaction.Complete();
                    
                    // log here to keep to avoid cluttering domain
                    logger.Debug("Successful friend recommendation");
                    StatsdClient.Metrics.Counter("friendReferrals");
                }
                catch (ReferralRejectedDueToLongTermOutstandingBalance ex)
                {
                    logger.Error(ex);
                    StatsdClient.Metrics.Counter("ReferralRejected");
                    throw new ApplicationError(
                        "Sorry, this referral cannot be completed. The referrer " +
                        "currently has an outstanding balance. Please contact " +
                        "customer support"
                    );
                    // transaction will roll back if Complete() not called
                }
            }
        }

        // technical validation carried out at the application level
        private void Validate(NewAccount account)
        {
            if (!account.Email.Contains("@"))
                throw new ValidationFailure("Not a valid email address");

            if (account.Email.Length >= 50)
                throw new ValidationFailure("Email address must be less than 50 characters");

            if (account.Nickname.Length >= 25)
                throw new ValidationFailure("Nickname must be less than 25 characters");

            if (String.IsNullOrWhiteSpace(account.Email))
                throw new ValidationFailure("You must supply an email");

            if (String.IsNullOrWhiteSpace(account.Nickname))
                throw new ValidationFailure("You must supply a Nickname");
        }
    }

    public class ValidationFailure : Exception
    {
        public ValidationFailure(string message) : base(message) { }
    }

    public class ApplicationError : Exception
    {
        public ApplicationError(string message) : base(message) { }
    }

    public class ReferralRejectedDueToLongTermOutstandingBalance : Exception { }
}

// just to exemplify that these classes belong to the domain
namespace Domain
{
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

    public interface ICustomerDirectory
    {
        Customer Find(int customerId);

        Customer Add(NewAccount account);
    }

    public interface IReferAFriendPolicy
    {
        void Apply(Customer referrer, Customer friend);
    }
}