using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Text;
using System.IO;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountManageent.RegularAccounts.BeganFollowing
{
    public class BeganFollowingController : ApiController
    {
        private const string BeganFollowingBaseUrl = "http://localhost:4102/";

        [HttpGet]
        public HttpResponseMessage Feed()
        {
            // create feed
            var feedUri = new Uri(BeganFollowingBaseUrl + "beganfollowing");
            var feed = new SyndicationFeed(
"BeganFollowing", "Began following domain events", feedUri
         );
            feed.Authors.Add(
new SyndicationPerson("accountManagementBC@WroxPPPDDD.com")
     );
            feed.Items = EventRetriever.RecentEvents("BeganFollowing")
     .Select(MapToFeedItem);

            // set feed as response - always atom+xml - no HAL
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(
        GetFeedContent(feed), Encoding.UTF8, "application/atom+xml"
     );
            return response;
        }

        private string GetFeedContent(SyndicationFeed feed)
        {
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw))
            {
                feed.GetAtom10Formatter().WriteTo(xw);
                xw.Flush();

                return sw.ToString();
            }
        }

        private SyndicationItem MapToFeedItem(ResolvedEvent ev)
        {
            return new SyndicationItem(
                "BeganFollowingEvent",
                Encoding.UTF8.GetString(ev.Event.Data),
  new Uri(RequestContext.Url.Content("/beganfollowing/" +
    ev.Event.EventId)),
  ev.Event.EventId.ToString(),
                DateTime.Now // Event store client does not return timestamp yet
            );
        }
    }

    public static class EventRetriever
    {
        private static IPEndPoint defaultEsEndpoint = new IPEndPoint(IPAddress.Loopback, 1113);
        private static IEventStoreConnection esConn = EventStoreConnection.Create(defaultEsEndpoint);

        static EventRetriever()
        {
            esConn.Connect();
        }

        public static IEnumerable<ResolvedEvent> RecentEvents(string stream)
        {
            var results = esConn.ReadStreamEventsForward(stream, 0, 20, false);
            return results.Events;
        }
    }

}
