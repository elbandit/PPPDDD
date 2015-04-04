using PPPDDDChap18.DomainServices.Shipping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.Shipping.Application
{
    // implementation of Domain Service - this would live in the Service Layer
    public class ShippingRouteFinder : IShippingRouteFinder
    {
        public Route FindFastestRoute(Location departing, Location destination, DateTime departureDate)
        {
            // this method makes HTTP call; best to keep out of the domain
            var response = QueryRoutingApi(departing, destination, departureDate);

            var route = ParseRoute(response);

            return route;
        }

        private String QueryRoutingApi(Location departing, Location destination, DateTime departureDate)
        {
            // http calls etc
            return null;
        }

        private Route ParseRoute(String apiResponse)
        {
            return null;
        }
    }
}
