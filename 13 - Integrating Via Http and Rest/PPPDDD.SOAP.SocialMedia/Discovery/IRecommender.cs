using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Discovery
{
    [ServiceContract]
    public interface IRecommender
    {
        [OperationContract]
        List<string> GetRecommendedUsers(string accountId);
    }
}
