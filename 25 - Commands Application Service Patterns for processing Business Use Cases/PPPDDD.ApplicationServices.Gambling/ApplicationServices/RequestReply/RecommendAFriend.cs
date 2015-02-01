using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using NewAccount = Domain.NewAccount;
using PPPDDD.ApplicationServices.Gambling.ApplicationServices.ErrorHandling;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.RequestReply
{
    public class RecommendAFriendService
    {
        private IReferralPolicy policy;

        public RecommendAFriendService(Domain.IReferralPolicy policy)
        {
            this.policy = policy;
        }

        public RecommendAFriendResponse RecommendAFriend(RecommendAFriendRequest request)
        {
            try
            {
                var command = new RecommendAFriend
                {
                    ReferrerId = request.ReferrerId,
                    Friend = request.Friend
                };

                policy.Apply(command);

                return new RecommendAFriendResponse
                {
                    Status = RecommendAFriendStatus.Success
                };
            }
            catch (ReferralRejectedDueToLongTermOutstandingBalance)
            {
                return new RecommendAFriendResponse
                {
                    Status = RecommendAFriendStatus.ReferralRejected
                };
            }
        }
    }

    public class RecommendAFriendRequest
    {
        public int ReferrerId { get; set; }

        public NewAccount Friend { get; set; }
    }

    public class RecommendAFriendResponse
    {
        public RecommendAFriendStatus Status { get; set; }
    }

    public enum RecommendAFriendStatus
    {
        Success,
        ReferralRejected
    }
}
