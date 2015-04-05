using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model
{
    public static class RestaurantBookingFactory
    {
        // Initial object - no ORM to worry about here
        // but will this approach be compatible with an ORM?
        public static RestaurantBooking CreateBooking(Restaurant restaurant, Customer customer, BookingDetails details)
        {
            var id = Guid.NewGuid();
            var notifier = new RestaurantNotifier();
            
            // Redesign RestaurantBooking entity to suit this style of construction
            //return new RestaurantBooking(restaurant, customer, details, id, notifier);

            return null;
        }
    }
}
