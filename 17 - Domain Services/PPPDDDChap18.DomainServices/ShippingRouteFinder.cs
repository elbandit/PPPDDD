using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices
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

        // ...
    }

    // interface for Domain Service - this would live in the Domain Model
    // this is the "contract"
    public interface IShippingRouteFinder
    {
        Route FindFastestRoute(Location departing, Location destination, DateTime departureDate);
    }

    public class Route
    {
        // ...
    }

    public class Location
    {
        // ...
    }

}
