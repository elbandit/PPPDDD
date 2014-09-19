using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealershipPerformanceReportDomainForMediator;

namespace PPPDDD.Reporting.Controllers
{
    public class DealershipReportUsingMediatorController : Controller
    {
        private IDealershipRepository repository;

        public DealershipReportUsingMediatorController(IDealershipRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(IEnumerable<int> dealershipIds, DateTime start, DateTime end)
        {
            var dealerships = repository.Get(dealershipIds);
            var builder = new DealershipPerformanceReportBuilderUsingMediator();
            var viewModel = builder.BuildReport(dealerships, start, end);

            return View(viewModel);
        }
	}

    public class DealershipPerformanceReportBuilderUsingMediator
    {
        private IDealershipRevenueCalculator calculator;
        private IDealershipPerformanceTargetsProvider provider;

        public DealershipPerformanceReport BuildReport(IEnumerable<Dealership> dealerships, DateTime start, DateTime end)
        {
            var statuses = BuildStatuses(dealerships, start, end);

            return new DealershipPerformanceReport
            {
                Dealerships = statuses,
                ReportStartDate = start, 
                ReportEndDate = end
            };
        }

        private List<DealershipPerformanceStatus> BuildStatuses(IEnumerable<Dealership> dealerships, DateTime start, DateTime end)
        {
            var statuses = new List<DealershipPerformanceStatus>();
            foreach (var dealership in dealerships)
            {
                var status = new DealershipPerformanceStatus();
                var mediator = new DealershipAssessmentMediator(status); // Mediator wraps status

                var targets = provider.Get(dealership, start, end);
                var actuals = calculator.CalculateFor(dealership, start, end);

                // pass in the mediator, so private data can be set on the media
                targets.Populate(mediator); 
                actuals.Populate(mediator);

                statuses.Add(status); // values will have been set by the mediator when passed into domain objects
            }
            return statuses;
        }
    }

    // "mediator" suffix on class name used for demo clarity
    public class DealershipAssessmentMediator : IDealershipAssessment
    {
        private DealershipPerformanceStatus status;

        public DealershipAssessmentMediator(DealershipPerformanceStatus status)
        {
            this.status = status;
        }

        public int TotalRevenue 
        {
            get { return status.TotalRevenue;  }
            set { status.TotalRevenue = value;  }
        }

        public int TargetRevenue 
        {
            get { return status.TargetRevenue; }
            set { status.TargetRevenue = value; }
        }

        public int NetProfit 
        {
            get { return status.NetProfit;  }
            set { status.NetProfit = value;  }
        }

        public int TargetProfit 
        {
            get { return status.TargetProfit;  }
            set { status.TargetProfit = value;  }
        }
    }
    
}

// this would be a separate pure domain project
namespace DealershipPerformanceReportDomainForMediator
{
    // mediator interface - stable domain structure that can be exposed
    public interface IDealershipAssessment
    {
        int TotalRevenue { get; set; }

        int TargetRevenue { get; set; }

        int NetProfit { get; set; }

        int TargetProfit { get; set; }
    }

    public interface IDealershipRepository
    {
        Dealership Get(int dealershipId);

        IEnumerable<Dealership> Get(IEnumerable<int> dealershipIds);
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
        // private fields hide potentially volatile domain structure
        private int targetRevenue;
        private int targetProfit;

        public void Populate(IDealershipAssessment mediator)
        {
            mediator.TargetRevenue = targetRevenue;
            mediator.TargetProfit = targetProfit;
        }
    }

    public class DealershipPerformanceActuals
    {
        // private fields hide potentially volatile domain structure
        private int totalRevenue;
        private int netProfit;

        public void Populate(IDealershipAssessment mediator)
        {
            mediator.TotalRevenue = totalRevenue;
            mediator.NetProfit = netProfit;
        }
    }

    public class DealershipPerformance
    {
        // private fields hide potentially volatile domain structure
        private int totalRevenue;
        private int netProfit;

        public void Populate(IDealershipAssessment mediator)
        {
            mediator.TotalRevenue = totalRevenue;
            mediator.NetProfit = netProfit;
        }
    }

    public class Dealership
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}