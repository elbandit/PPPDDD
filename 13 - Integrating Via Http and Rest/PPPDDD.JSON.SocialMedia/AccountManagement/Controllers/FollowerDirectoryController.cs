using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountManagement.Controllers
{
    public class FollowerDirectoryController : ApiController
    {
        public IHttpActionResult GetUsersFollowers(string accountId)
        {
            var followers = GenerateDummyFollowers().ToList();
            return Json(followers);
        }

        private IEnumerable<Follower> GenerateDummyFollowers()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new Follower
                {
                    FollowerId = "follower_" + i,
                    FollowerName = "happy follower " + i,
                    SocialTags = new List<string> { "programming", "DDD", "Psychology" },
                };
            }
        }

    }

    public class Follower
    {
        public string FollowerId { get; set; }

        public string FollowerName { get; set; }

        public List<string> SocialTags { get; set; }
    }

}
