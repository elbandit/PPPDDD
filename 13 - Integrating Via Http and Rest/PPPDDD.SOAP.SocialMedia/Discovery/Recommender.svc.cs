using System;
using System.Linq;
using System.Collections.Generic;
using Discovery.AccountManagement;

namespace Discovery
{
    public class Recommender : IRecommender
    {
        public List<string> GetRecommendedUsers(string accountId)
        {
            var accountManagementBC = new AccountManagement.FollowerDirectoryClient();
            var followers = accountManagementBC.FindUsersFollowers(accountId);
            return FindRecommendedUsersBasedOnSocialTags(followers);
        }

        private List<string> FindRecommendedUsersBasedOnSocialTags(Follower[] followers)
        {
            /*
             * Real system would look at the users tags and find 
             * popular accounts with similar tags by making more 
             * RPC calls.
             */
            var tags = followers.SelectMany(f => f.SocialTags).Distinct();
            return tags.Select(t => t + "_user_1").ToList();
        }
    }
}
