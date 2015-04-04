using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model
{
    public class BookingConfirmedByCustomer
    {
        private DomainEventsAlternative.RestaurantBooking restaurantBooking;

        public BookingConfirmedByCustomer(RestaurantBooking booking)
        {
            this.RestaurantBooking = booking;
        }

        public BookingConfirmedByCustomer(DomainEventsAlternative.RestaurantBooking restaurantBooking)
        {
            // TODO: Complete member initialization
            this.restaurantBooking = restaurantBooking;
        }

        public RestaurantBooking RestaurantBooking { get; private set; }
    }
}
