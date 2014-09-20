using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealershipPerformanceReportDomain;

namespace PPPDDD.Reporting.Controllers
{
    public class DealershipPerformanceReportController : Controller
    {
        private DealershipPerformanceReportBuilder builder;

        public DealershipPerformanceReportController(IDealershipRepository repository,
            IDealershipRevenueCalculator calculator, IDealershipPerformanceTargetsProvider provider)
        {
            this.builder = new DealershipPerformanceReportBuilder(
                repository, calculator, provider
            );
        }

        public ActionResult Index(IEnumerable<int> dealershipIds, DateTime start, DateTime end)
        {
            var viewModel = builder.BuildReport(dealershipIds, start, end);

            return View(viewModel);
        }
	}

    // Application Service
    public class DealershipPerformanceReportBuilder
    {
        private IDealershipRepository repository;
        private IDealershipRevenueCalculator calculator;
        private IDealershipPerformanceTargetsProvider provider;

        public DealershipPerformanceReportBuilder(IDealershipRepository repository,
            IDealershipRevenueCalculator calculator, IDealershipPerformanceTargetsProvider provider)
        {
            this.repository = repository;
            this.calculator = calculator;
            this.provider = provider;
        }

        public DealershipPerformanceReport BuildReport(IEnumerable<int> dealershipIds, DateTime start, DateTime end)
        {
            var statuses = BuildStatuses(dealershipIds, start, end);

            return new DealershipPerformanceReport
            {
                ReportStartDate = start,
                ReportEndDate = end,
                Dealerships = statuses
            };
        }

        private List<DealershipPerformanceStatus> BuildStatuses(IEnumerable<int> dealershipIds, DateTime start, DateTime end)
        {
            var statuses = new List<DealershipPerformanceStatus>();
            foreach (var id in dealershipIds)
            {
                // select N+1 - potentially bad for performance and efficiency
                // re-using existing domain code - quick to implement
                var dealership = repository.Get(id);
                var targets = provider.Get(dealership, start, end);
                var actuals = calculator.CalculateFor(dealership, start, end);

                // map from domain to view model so UI is not coupled to domain objects
                // could move this logic into a separate mapper
                statuses.Add(new DealershipPerformanceStatus
                {
                    DealershipName = dealership.Name,
                    TotalRevenue = actuals.TotalRevenue,
                    TargetRevenue = targets.TargetRevenue,
                    NetProfit = actuals.NetProfit,
                    TargetProfit = targets.TargetProfit
                });
            }
            return statuses;
        }
    }

    // view model
    public class DealershipPerformanceReport
    {
        public DateTime ReportStartDate { get; set; }

        public DateTime ReportEndDate { get; set; }

        public List<DealershipPerformanceStatus> Dealerships { get; set; }
    }

    public class DealershipPerformanceStatus
    {
        public string DealershipName { get; set; }

        public int TotalRevenue { get; set; }

        public int TargetRevenue { get; set; }

        public int NetProfit { get; set; }

        public int TargetProfit { get; set; }
    }
}

// this would be a separate project
namespace DealershipPerformanceReportDomain
{
    public interface IDealershipRepository
    {
        Dealership Get(int dealershipId);
    }
    
    public interface IDealershipRevenueCalculator
    {
        DealershipPerformanceActuals CalculateFor(Dealership dealership, DateTime start, DateTime end);
    }

    public interface IDealershipPerformanceTargetsProvider
    {
        DealershipPerformanceTargets Get(Dealership dealership, DateTime start, DateTime end);
    }

    public class DealershipPerformanceTargets
    {
        public int TargetRevenue { get; set; }

        public int TargetProfit  { get; set; }
    }

    public class DealershipPerformanceActuals
    {
        public int TotalRevenue { get; set; }

        public int NetProfit { get; set; }
    }

    public class Dealership
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}