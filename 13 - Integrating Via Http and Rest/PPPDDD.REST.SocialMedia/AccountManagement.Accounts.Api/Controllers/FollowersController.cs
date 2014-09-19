using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Hal;
using EventStore.ClientAPI;
using System.Net;
using System.Text;
using ServiceStack.Text;
using System.Net.Http;

namespace AccountManagement.Accounts.Controllers
{
    public class FollowersController : ApiController
    {
        private const string AccountsBaseUrl = "http://localhost:4101/accountmanagement/";

        [HttpGet]
        public FollowersRepresentation Index(string accountId)
        {
            return new FollowersRepresentation
            {
                Href = AccountsBaseUrl + "accounts/" + accountId + "/followers",
                Rel = "self",
                Links = new List<Link>
                {
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts/" + accountId + "/followers?pages=2",
                        Rel = "next",
                    },
                },
                followers = GetFollowers(accountId)
            };
        }

        private List<Follower> GetFollowers(string accountId)
        {
            // replace with DB lookup etc
            return new List<Follower>
            {
                new Follower 
                {
                    AccountId = "fl1",
		         },
                new Follower 
                {
                    AccountId = "fl2",
		        },
                new Follower 
                {
                    AccountId = "fl3",
		        }
            };
        }

        [HttpPost] // respond to POST requests only
        [ActionName("index")] // Web API will not allow duplicate names
        public IHttpActionResult IndexPOST(string accountId, Follower follower)
        {
            // accountId will be taken from querystring - it is a simple type
            // follower will be taken from request body - it is a complex type

            var evnt = new BeganFollowing
            {
                AccountId = accountId,
                FollowerId = follower.AccountId
            };
            EventPersister.PersistEvent(evnt);
            return RedirectToRoute("Account Followers", new { accountId = accountId });
        }
    }

    public class FollowersRepresentation : Representation
    {
        public List<Follower> followers { get; set; }

        protected override void CreateHypermedia()
        {
        }
    }

    public class Follower
    {
        public string AccountId { get; set; }
    }

    // representing the domain event
    public class BeganFollowing
    {
        public string AccountId { get; set; }

        public string FollowerId { get; set; }
    }

    public static class EventPersister
    {
        private static IPEndPoint defaultEsEndpoint =
     new IPEndPoint(IPAddress.Loopback, 1113);

        private static IEventStoreConnection esConn =
        EventStoreConnection.Create(defaultEsEndpoint);

        static EventPersister()
        {
            esConn.Connect();
        }

        public static void PersistEvent(object ev)
        {
            var commitHeaders = new Dictionary<string, object>
            {
                {"CommitId", Guid.NewGuid()},
            };

            esConn.AppendToStream(
                "BeganFollowing", ExpectedVersion.Any, ToEventData(Guid.NewGuid(),
                ev, commitHeaders
            ));
        }

        private static EventData ToEventData(Guid eventId, object evnt, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(
                JsonSerializer.SerializeToString(evnt)
            );
            var metadata = Encoding.UTF8.GetBytes(
                JsonSerializer.SerializeToString(headers)
            );
            var typeName = evnt.GetType().Name;
            
            return new EventData(eventId, typeName, true, data, metadata);
        }
    }
}
