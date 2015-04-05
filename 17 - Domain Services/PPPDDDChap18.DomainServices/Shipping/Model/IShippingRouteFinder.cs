using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.Shipping.Model
{
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
