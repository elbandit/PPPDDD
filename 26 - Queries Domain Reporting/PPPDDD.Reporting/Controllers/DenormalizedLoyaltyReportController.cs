using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;

namespace PPPDDD.Reporting.Controllers
{
    public class DenormalizedLoyaltyReportController : Controller
    {
        //
        // GET: /DenormalizedLoyaltyReport/
        public ActionResult Index()
        {
            return View();
        }

        public class DenormalizedLoyaltyReportBuilder
        {
            private string connString = "";

            public LoyaltyReport Build(DateTime start, DateTime end)
            {
                IEnumerable<LoyaltySummary> summaries;
                using(var con = new SqlConnection(connString))
                {
                    con.Open();
                    var query = "select [Month], PointsPerDollar, NetProfit, Signups, Purchases " +
                                "from denormalizedLoyaltyReportViewCache " +
                                "where [Month] >= @start " +
                                "and [Month] < @end";
                    summaries = con.Query<LoyaltySummary>(query, new { start = start, end = end });
                }

                return new LoyaltyReport
                {
                    Summarries = summaries
                };
            }
        }
	}
}