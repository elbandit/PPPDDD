using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PPPDDD.Reporting.Controllers
{
    public class HealthcareEventProjectionReportController : Controller
    {
        // Run the application and browser to this url "/HealthcareProjectionReport/TestData" to insert test data for this demo
        public ActionResult TestData()
        {
            InsertTestData.Insert();
            return Content("Test Data inserted. Navigate to: http://localhost:2113/web/streams.htm");
        }

        // Example: http://localhost:{yourPort}/HealthcareEventProjectionReport?start=2014/01&end=2014/03&diagnosisIds=dg1
        public ActionResult Index(DateTime start, DateTime end, string diagnosisIds)
        {
            var reportViewModel = new HealthcareReportBuilder().Build(start, end, diagnosisIds.Split(','));

            // Optional: Create a view and display the report
            return Json(reportViewModel, JsonRequestBehavior.AllowGet);
        }
	}

    // Report building Application Service
    public class HealthcareReportBuilder
    {
        public HealthcareReport Build(DateTime start, DateTime end, IEnumerable<string> diagnosisIds)
        {
            var monthsInReport = GetMonthsInRange(start, end).ToList(); // report columns
            var monthlyOverallTotals = FetchMonthlyTotalsFromES(monthsInReport); // used to calculate percentages
            var queries = BuildQueriesFor(monthsInReport, diagnosisIds).ToList(); // queries for ES
            var summaries = BuildMonthlySummariesFor(queries, monthlyOverallTotals).ToList(); // use queries
            
            return new HealthcareReport
            {
                Start = start,
                End = end,
                Summaries = summaries
            };
        }

        private IEnumerable<DiagnosisSummary> BuildMonthlySummariesFor(IEnumerable<DiagnosisQuery> queries, 
            Dictionary<DateTime, int> monthlyTotals)
        {
            foreach (var q in queries) // may want to run these in parallel for perf
            {
                var diagnosisTotal = FetchTotalFromESFor(q);
                var monthTotal = monthlyTotals[q.Month];
                var percent = monthTotal == 0 ? 0 : ((decimal)diagnosisTotal / monthTotal) * 100;

                yield return new DiagnosisSummary
                {
                    Amount = diagnosisTotal,
                    DiagnosisName = GetDiagnosisName(q.DiagnosisId),
                    Month = q.Month,
                    Percentage = percent,
                    MonthString = q.Month.ToString("yyyy/MM")
                };
            }
        }

        private int FetchTotalFromESFor(DiagnosisQuery query)
        {
            // don't hard-code this URL. Access via entry point resource
            var projectionStateUrl = "http://localhost:2113/projection/DiagnosesByMonthCounts/state";
            var streamname = "diagnosis-" + query.DiagnosisId + "_" + query.Month.ToString("yyyyMM");
            
            // may want to use caching here
            var response = new WebClient().DownloadString(projectionStateUrl + "?partition=" + streamname);
            var count = Json.Decode<DiagnosisCount>(response);
            
            return count == null ? 0 : count.Count;
        }

        private Dictionary<DateTime, int> FetchMonthlyTotalsFromES(IEnumerable<DateTime> months)
        {
            // don't hard-code this URL. Access via entry point resource
            var projectionStateUrl = "http://localhost:2113/projection/MonthsCounts/state";

            var totals = new Dictionary<DateTime, int>();
            foreach(var m in months)
            {
                var streamName = "month-" + m.ToString("yyyyMM");
                var url = projectionStateUrl + "?partition=" + streamName;
                var response = new WebClient().DownloadString(url);
                var count = Json.Decode<DiagnosisCount>(response);

                totals.Add(m, count == null ? 0 : count.Count);
            }

            return totals;
        }

        private IEnumerable<DiagnosisQuery> BuildQueriesFor(IEnumerable<DateTime> months, IEnumerable<string> diagnosisIds)
        {
            foreach (var month in months)
            {
                foreach (var id in diagnosisIds)
                {
                    yield return new DiagnosisQuery
                    {
                        DiagnosisId = id,
                        Month = month,
                    };
                }
            }
        }

        // this would problably live inside a helper, and not in an application service
        private IEnumerable<DateTime> GetMonthsInRange(DateTime start, DateTime end)
        {
            var startOfFirst = new DateTime(start.Year, start.Month, 1);
            var lastOfEnd = new DateTime(end.Year, end.Month + 1, 1);
            var current = startOfFirst;
            do
            {
                yield return current;
                current = current.AddMonths(1);
            } while (current < lastOfEnd);
        }

        private string GetDiagnosisName(string diagnosisId)
        {
            // many ways to implements this:
                // could come from an event stream
                // could live as a fixed list in cache if diagnosis never change 
                // could be a lookup from a datastore
            switch(diagnosisId)
            {
                case "dg1": { return "Eczema"; break; };
                case "dg2": { return "Vertigo"; break ;};
                case "dg3": { return "Hypochrondriac"; break; };
                default: { return "Unknown"; }
            }
        }
    }

    // represents state projection
    public class DiagnosisCount
    {
        public int Count { get; set; }
    }

    public class DiagnosisQuery
    {
        public DateTime Month { get; set; }

        public string DiagnosisId { get; set; }
    }
    

    // report view models
    public class HealthcareReport
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public IEnumerable<DiagnosisSummary> Summaries { get; set; }
    }

    public class DiagnosisSummary
    {
        public string DiagnosisName { get; set; }

        public DateTime Month { get; set; }

        public string MonthString { get; set; }

        public int Amount { get; set; }

        public decimal Percentage { get; set; }
    }

    /*
     * Inserts test diagnosis events into Event Store.
     * Requires event store to be running on port 2113. 
     */
    public static class InsertTestData
    {
        private static string diagnosesStreamUrl = "http://localhost:2113/streams/diagnoses";
        public static void Insert()
        {
            var request = (HttpWebRequest)WebRequest.Create(diagnosesStreamUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            
            var json = Json.Encode(DiagnosisEvents);
           
            using (var sr = new StreamWriter(request.GetRequestStream()))
            {
                sr.Write(json);
                sr.Flush();
                sr.Close();
            }
            request.GetResponse();
        }

        // model of an event - used to push data into ES not used for the report
        private class Diagnosis
        {
            public Guid EventId { get; set; }

            public string EventType { get; set; }

            public DiagnosisData data { get; set; }
        }

        private class DiagnosisData
        {
            public string DiagnosisId { get; set; }

            public string DiagnosisName { get; set; }

            public string DoctorId { get; set; }

            public string DoctorName { get; set; }

            public string Date { get; set; }
        }

        private static IEnumerable<Diagnosis> DiagnosisEvents = new List<Diagnosis>
        {
            new Diagnosis
            {
                EventId = Guid.NewGuid(),
                EventType = "diagnosis",
                data = new DiagnosisData
                {
                    DiagnosisId = "dg1",
                    DiagnosisName = "Eczema",
                    DoctorId = "doc1",
                    DoctorName = "D.C. Green",
                    Date = "2014/02"
                }
            },
            new Diagnosis
            {
                EventId = Guid.NewGuid(),
                EventType = "diagnosis",
                data = new DiagnosisData
                {
                    DiagnosisId = "dg2",
                    DiagnosisName = "Vertigo",
                    DoctorId = "doc1",
                    DoctorName = "D.C. Green",
                    Date = "2014/02"
                }
            },
            new Diagnosis
            {
                EventId = Guid.NewGuid(),
                EventType = "diagnosis",
                data = new DiagnosisData
                {
                    DiagnosisId = "dg1",
                    DiagnosisName = "Eczema",
                    DoctorId = "doc2",
                    DoctorName = "J.P. Finch",
                    Date = "2014/03"
                }
            },
            new Diagnosis
            {
                EventId = Guid.NewGuid(),
                EventType = "diagnosis",
                data = new DiagnosisData
                {
                    DiagnosisId = "dg1",
                    DiagnosisName = "Eczema",
                    DoctorId = "doc2",
                    DoctorName = "J.P. Finch",
                    Date = "2014/03"
                }
            },
            new Diagnosis
            {
                EventId = Guid.NewGuid(),
                EventType = "diagnosis",
                data = new DiagnosisData
                {
                    DiagnosisId = "dg3",
                    DiagnosisName = "Hypochondriac",
                    DoctorId = "doc3",
                    DoctorName = "U.B Retters",
                    Date = "2014/04"
                }
            },
        };
    }

    /*
        Event Store Projections:
        
        DiagnosesByMonth:
        fromStream('diagnoses')
        .whenAny(function(state, ev) {
            var date = ev.data.Date.replace('/', '');
            var diagnosisId = ev.data.DiagnosisId;
            linkTo('diagnosis-' + diagnosisId + '_' + date, ev);
        });
        
        DiagnosesByMonthCounts:
        fromCategory('diagnosis')
        .foreachStream()
        .when({ 
             $init : function(s,e) {return {count : 0}},
             "diagnosis" : function(s,e) { s.count += 1} //mutate in place works
        });
      
        Months:
        fromStream('diagnoses')
        .whenAny(function(state, ev) {
            var date = ev.data.Date.replace('/', '');
            linkTo('month-' + date, ev);
        });
     
        MonthsCounts:
        fromCategory('month')
        .foreachStream()
        .when({
            $init : function(s,e) {return {count : 0}},
            "diagnosis" : function(s,e) { s.count += 1 } //mutate in place works
        }); 
      
     */


}