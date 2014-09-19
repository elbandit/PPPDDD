using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AccountManagement
{
    public class FollowerDirectory : IFollowerDirectory
    {
        public List<Follower> FindUsersFollowers(string accountId)
        {
            return GenerateDummyFollowers().ToList();
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
}
