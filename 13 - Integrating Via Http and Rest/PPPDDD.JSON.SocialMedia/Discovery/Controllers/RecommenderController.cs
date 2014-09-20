using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceStack.Text;

namespace Discovery.Controllers
{
    public class RecommenderController : ApiController
    {
        public List<string>  GetRecommendedUsers(string accountId)
        {
            var accountManagementUrl =
                "http://localhost:3200/api/" +
                "followerdirectory/getusersfollowers?" +
                "accountId=" + accountId;

            var response = new WebClient().DownloadString(accountManagementUrl);
            var followers = JsonSerializer.DeserializeFromString<List<Follower>>(response);
		
            // automatically converted to JSON by Web API
            return FindRecommendedUsersBasedOnSocialTags(followers);
        }

        private List<string> FindRecommendedUsersBasedOnSocialTags(List<Follower> followers)
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

    /* class not shared between Bounded Contexts to meet
     * requirement of no source code dependencies
     */
    public class Follower
    {
        public string FollowerId { get; set; }

        public string FollowerName { get; set; }

        public List<string> SocialTags { get; set; }
    }
}
