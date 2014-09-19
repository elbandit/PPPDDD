using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Threading.Tasks;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.PublishSubscribeAsync
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

        public void RecommendAFriend(int referrerId, NewAccount friend)
        {
            // validation, open transaction etc
            var command = new RecommendAFriend
            {
                ReferrerId = referrerId,
                Friend = friend
            };
            Task.Factory.StartNew(() => policy.Apply(command));
            // close transaction - success and failure handled in handlers
        }
    }
}

