using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.CommandProcessor
{
    public class BloatedRefcommendAFriendService
    {
        public void RecommendAFriend(int referrerId, NewAccount friend)
        {
            // ...
        }

        public void RecommendAFriendInDifferentCountry(int referrerId, NewAccount friend)
        {
            // ...
        }

        public void ReverseFriendReferral(int referrerId, int friendId)
        {
            // ...
        }

        public void ReferAFriendWithoutLoyaltyBonus(int referrerId, NewAccount friend)
        {
            // ...
        }

        // .... more methods like this
    }
}