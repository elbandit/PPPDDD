using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model
{
    /*
    public class RestaurantBookingRepository
    {
        // interface provided by ORM
        private ISession session;

        public RestaurantBookingRepository(ISession session)
        {
            this.session = session;
        }

        public RestaurantBooking Get(Guid restaurantBookingId)
        {
            // first phase of construnction handled by ORM
            var booking = session.Load<RestaurantBooking>(restaurantBookingId);

            // second phase of construction manually handled
            booking.Init(new RestaurantNotifier());
            // booking.Notifier = new RestaurantNotifier();
        }
    }
     */
}
