using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model
{
    // Domain Service
    public class RestaurantNotifier : IRestaurantNotifier
    {

        public bool NotifyBookingConfirmation(Restaurant Restaurant, Customer Customer, Guid Id, BookingDetails BookingDetails)
        {
            throw new NotImplementedException();
        }
    }

    public interface IRestaurantNotifier
    {
        bool NotifyBookingConfirmation(Restaurant Restaurant, Customer Customer, Guid Id, BookingDetails BookingDetails);
    }
}
