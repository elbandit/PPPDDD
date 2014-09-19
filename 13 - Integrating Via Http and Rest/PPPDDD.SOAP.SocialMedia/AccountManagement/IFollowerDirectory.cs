using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AccountManagement
{
    [ServiceContract]
    public interface IFollowerDirectory
    {
        [OperationContract]
        List<Follower> FindUsersFollowers(string accountId);
    }

    public class Follower
    {
        public string FollowerId { get; set; }

        public string FollowerName { get; set; }

        public List<string> SocialTags { get; set; }
    }
}
