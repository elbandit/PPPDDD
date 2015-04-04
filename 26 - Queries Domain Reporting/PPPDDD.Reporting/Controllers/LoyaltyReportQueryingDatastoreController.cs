using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// right-click on the "SportsStoreDatabase" project and choose "publish"
// this will create a local version of the database on this machine
namespace PPPDDD.Reporting.Controllers
{
    public class LoyaltyReportQueryingDatastoreController : Controller
    {
        public ActionResult Index(DateTime start, DateTime end)
        {
            var report = new LoyaltyReportBuilder().Build(start, end);

            return View(report);
        }
	}

    // Application Service
    public class LoyaltyReportBuilder
    {
        // see SportsStoreDatabase project in this solution
        private string connString = "";

        public LoyaltyReport Build(DateTime start, DateTime end)
        {
            IEnumerable<PurchasesAndProfit> profits;
            IEnumerable<SignupCount> signups;
            IEnumerable<LoyaltySettings> settings;

            using(var con = new SqlConnection(connString))
            {
                con.Open();
                var pointsQuery = "select [Month], [PointsPerDollar] from loyaltySettings " +
                                  "where [Month] >= @start " + 
                                  "and [Month] < @end";
                settings = con.Query<LoyaltySettings>(pointsQuery, new { start = start, end = end });

                var signupsQuery = "select count(*) from loyaltyAccounts" +
                                   "where isActive = true " +
                                   "and [created] >= @start " +
                                   "and [created] < @end ";
                signups = con.Query<SignupCount>(signupsQuery, new { start = start, end = end } );

                var profitQuery = "select " +
                                 "      concat(month(o.[date]), '/', year(o.[date])) as Month, " +
                                 "      (select ((cast(count(*) as decimal) / (" +
                                 "          select count(*) from orders" +
                                 "          where [date] >= @start" +
                                 "          and [date] <  @end" +
                                 "      )) * 100)) as Purchases," +
                                 "      (select ((sum(netProfit) / (" +
                                 "          select sum(netProfit) from orders" +
                                 "          where [date] >= @start" +
                                 "          and [date] < @end " +
                                 "      )) * 100)) as NetProfit" +
                                 "  from orders o" +
                                 "  join Users u on o.userId = u.id" +
                                 "  join LoyaltyAccounts la on u.id = la.userId" +
                                 "  where la.isActive = 1" +
                                 "  and o.[date] >= @start" +
                                 "  and o.[date] < @end" +
                                 "  group by concat(month(o.[date]), '/', year(o.[date]))";
                profits = con.Query<PurchasesAndProfit>(profitQuery, new { start = start, end = end });
            }

            return Map(profits, signups, settings, start, end);
        }

        private LoyaltyReport Map(IEnumerable<PurchasesAndProfit> profits, IEnumerable<SignupCount> signups, 
            IEnumerable<LoyaltySettings> loyaltySettings, DateTime start, DateTime end)
        {
            var summaries = new List<LoyaltySummary>();

            // Create a summary for each month in the report's range
            foreach (var month in MonthsBetweenInclusive(start, end))
            {
                var monthsProfits = profits.Single(s => s.Month == month);
                var monthsSettings = loyaltySettings.Single(s => s.Month == month);
                var monthsSignups = signups.Single(s => s.Month == month);
                
                var summary = new LoyaltySummary
                {
                    Month = month,
                    NetProfit = monthsProfits.Profit,
                    PointsPerDollar = monthsSettings.PointsPerDollar,
                    Purchases = monthsProfits.Purchases,
                    SignUps = monthsSignups.Signups
                };

                summaries.Add(summary);
            }
                
            return new LoyaltyReport
            {
                Summarries = summaries
            };
        }

        private IEnumerable<DateTime> MonthsBetweenInclusive(DateTime start, DateTime end)
        {
            var firstMonth = new DateTime(start.Year, start.Month, 1);
            var lastMonth = new DateTime(end.Year, end.Month, 1);

            var months = new List<DateTime>();

            var currentMonth = firstMonth;
            while(currentMonth < lastMonth)
            {
                months.Add(currentMonth);
                currentMonth = currentMonth.AddMonths(1);
            }

            return months;
        }
    }

    // view / presentation models
    public class LoyaltyReport
    {
        public IEnumerable<LoyaltySummary> Summarries { get; set; }
    }

    public class LoyaltySummary
    {
        public DateTime Month { get; set; }

        public int PointsPerDollar { get; set; }

        public double NetProfit { get; set; }

        public int SignUps { get; set; }

        public int Purchases { get; set; }
    }

    // database models
    public class LoyaltySettings
    {
        public DateTime Month { get; set; }

        public int PointsPerDollar { get; set; }
    }

    public class SignupCount
    {
        public DateTime Month { get; set; }

        public int Signups { get; set; }
    }

    public class PurchasesAndProfit
    {
        public DateTime Month { get; set; }

        public int Purchases { get; set; }

        public double Profit { get; set; }
    }
}