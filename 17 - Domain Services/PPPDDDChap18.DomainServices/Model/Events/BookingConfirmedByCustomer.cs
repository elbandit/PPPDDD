using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.Model.Events
{
    public class BookingConfirmedByCustomer
    {
        public BookingConfirmedByCustomer(RestaurantBooking booking)
        {
            this.RestaurantBooking = booking;
        }

        public RestaurantBooking RestaurantBooking { get; private set; }
    }
}
