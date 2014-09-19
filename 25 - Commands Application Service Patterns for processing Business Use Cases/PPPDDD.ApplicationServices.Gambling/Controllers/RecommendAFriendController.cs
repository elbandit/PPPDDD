using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControllerDomain;
using System.Threading.Tasks;

namespace PPPDDD.ApplicationServices.Gambling.Controllers
{
    public class RecommendAFriendController : Controller
    {
        private ICustomerDirectory directory;
        private IReferAFriendPolicy policy;

        public RecommendAFriendController(
            ICustomerDirectory customerDirectory, IReferAFriendPolicy policy)
        {
            this.directory = customerDirectory;
            this.policy = policy;
        }

        // all infrastructure concerns handled by framework
        public ActionResult Index(int referrerId, NewAccount friend)
        {
            var referrer = directory.Find(referrerId);
            var newAcct = directory.Create(friend);
            policy.Apply(referrer, newAcct);

            return View();
        }
	}
}

namespace ControllerDomain
{
    public interface ICustomerDirectory
    {
        Customer Find(int customerId);

        Customer Create(NewAccount details);
    }

    public class NewAccount
    {
        public string Email { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
    }

    public interface IReferAFriendPolicy
    {
        void Apply(Customer referrer, Customer friend);
    }
}