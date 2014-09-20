using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.CommandProcessor
{
    // command expressing intent
    public class RecommendAFriend
    {
        public int ReferrerId { get; set; }

        public NewAccount Friend { get; set; }
    }

    public interface IRecommendAFriendProcessor
    {
        void Process(RecommendAFriend command);
    }

    public class NewAccount
    {
        public string Email { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }
    }
}