using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using ServiceStack.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Discovery.Reccommendations.Followers
{
    public class BeganFollowingPollingFeedConsumer
    {
        const string EntryPointUrl = "http://localhost:4100/accountmanagement";

        private static string LastEventIdProcessed;

        public static void Main(string[] args)
        {
            // probably not a production quality implementation
            while (true)
            {
                FetchAndProcessNextBatchOfEvents();
                Thread.Sleep(1000);
            }

        }

        private static void FetchAndProcessNextBatchOfEvents()
        {
            var atomFeed = FetchFeed();
            var ups = GetUnprocessedEvents(atomFeed.Items.ToList());

            if (ups.Any())
                ProcessEvents(ups);
            else
                Console.WriteLine("No new events to  process");
        }

        private static SyndicationFeed FetchFeed()
        {
            using(var wc = new WebClient())
            {
                // get the feed URI from the entry point resource
                var rawEp = wc.DownloadString(EntryPointUrl);
                var hal = JsonSerializer.DeserializeFromString<HALResource>(rawEp);
                var feedUrl = hal._links["beganfollowing"].href;

                // parse a strongly-typed syndication feed
                var rawFeed = wc.DownloadString(feedUrl);
                var feedXmlReader = XDocument.Parse(rawFeed).CreateReader();
                return SyndicationFeed.Load(feedXmlReader);
            };
        }

        private static List<SyndicationItem> GetUnprocessedEvents(List<SyndicationItem> events)
        {
            var lastProcessed = events.SingleOrDefault(s => s.Id == LastEventIdProcessed);
            var indexOfLastProcessedEvent = events.IndexOf(lastProcessed);

            return events.Skip(indexOfLastProcessedEvent + 1).ToList();
        }

        private static void ProcessEvents(List<SyndicationItem> events)
        {
            foreach (var ev in events)
            {
                var evnt = ParseEvent(ev.Content);
                Console.WriteLine("Processing event: " + evnt.AccountId + " - " + evnt.FollowerId);
                // Your domain rules here
                LastEventIdProcessed = ev.Id;
            }
        }

        private static BeganFollowing ParseEvent(SyndicationContent content)
        {
            // reference to servicestack.text
            var jsonString = ParseFeedContent(content);
            var bf = JsonSerializer.DeserializeFromString<BeganFollowing>(jsonString);
            return bf;
        }

        private static string ParseFeedContent(SyndicationContent syndicationContent)
        {
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw))
            {
                syndicationContent.WriteTo(xw, "BF", "BF");
                xw.Flush();

                return XDocument.Parse(sw.ToString()).Root.Value;
            }
        }
    }

    public class HALResource
    {
        public Dictionary<string, Link> _links { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
    }

    public class BeganFollowing
    {
        public string AccountId { get; set; }
        public string FollowerId { get; set; }
    }

}
