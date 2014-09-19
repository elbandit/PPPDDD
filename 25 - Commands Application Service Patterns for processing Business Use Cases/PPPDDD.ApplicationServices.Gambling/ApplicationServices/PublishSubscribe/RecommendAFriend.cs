using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.PublishSubscribe
{
    public class RecommendAFriendService
    {
        private IReferralPolicy policy;

        public RecommendAFriendService(Domain.IReferralPolicy policy)
        {
            // subscribe to events on domain model
            policy.ReferralAccepted += HandleReferralAccepted;
            policy.ReferralRejected += HandleReferralRejected;

            this.policy = policy;
        }

        private void HandleReferralAccepted(object sender, Domain.Referral e)
        {
            // send confirmation emails etc
        }

        private void HandleReferralRejected(object sender, Domain.Referral e)
        {
            // send rejection emails etc
        }

        public void ReferAFriend(int referrerId, NewAccount friend)
        {
            // validation, open transaction etc
            var command = new RecommendAFriend
            {
                ReferrerId = referrerId,
                Friend = friend
            };
            policy.Apply(command);
            // close transaction - success and failure handled in handlers
        }
    }
}

// named appropriately after your domain and living in another project
namespace Domain
{
    public interface IReferralPolicy
    {
        event EventHandler<Referral> ReferralAccepted;

        event EventHandler<Referral> ReferralRejected;

        void Apply(RecommendAFriend command);
    }

    public class Referral
    {
        public int ReferrerId { get; set; }

        public int FriendId { get; set; }
    }

    public class RecommendAFriend
    {
        public int ReferrerId { get; set; }

        public NewAccount Friend { get; set; }
    }
}