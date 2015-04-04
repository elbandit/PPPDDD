using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model.Handlers
{
    // Domain Event Handler
    public class NotifyRestaurantOnCustomerBookingConfirmation : IHandleEvents<BookingConfirmedByCustomer>
    {
        private IRestaurantNotifier restaurantNotifier;

        public NotifyRestaurantOnCustomerBookingConfirmation(IRestaurantNotifier restaurantNotifier)
        {
            this.restaurantNotifier = restaurantNotifier;
        }

        public void Handle(BookingConfirmedByCustomer @event)
        {
            // now the restaurant does not control it's own state as much
            var booking = @event.RestaurantBooking;

            /* Confirmed would need to be made public for this approach to work
            booking.Confirmed = restaurantNotifier.NotifyBookingConfirmation(booking.Restaurant,
                booking.Customer, booking.Id, booking.BookingDetails);
             */
        }
    }
}
