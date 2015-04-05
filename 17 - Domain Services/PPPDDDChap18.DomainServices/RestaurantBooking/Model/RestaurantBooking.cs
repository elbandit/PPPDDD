using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model
{
    public class RestaurantBooking
    {
        // see entities chapter for options when injecting services
        private IRestaurantNotifier restaurantNotifier = null;

        public Guid Id { get; protected set; }

        public Restaurant Restaurant { get; protected set; }

        public Customer Customer { get; protected set; }

        public BookingDetails BookingDetails { get; protected set;}

        public bool Confirmed { get; protected set; }

        // Entity behavior that depends on a Domain Service (see alternative implementations below)
        public void ConfirmBooking()
        {
            // ..

            // restaurantNotifier is the Domain Service
            // but how can it be available in this scope? especially when an ORM is involved?
            Confirmed = restaurantNotifier.NotifyBookingConfirmation(Restaurant, Customer, Id, BookingDetails);

            // ..
        }
       
        // ..
    }

    namespace DoubleDispatchAlternative
    {
        // Entity
        public class RestaurantBooking
        {
            // see entities chapter for options when injecting services
            private IRestaurantNotifier restaurantNotifier = null;

            public Guid Id { get; protected set; }

            public Restaurant Restaurant { get; protected set; }

            public Customer Customer { get; protected set; }

            public BookingDetails BookingDetails { get; protected set; }

            public bool Confirmed { get; protected set; }

            public void ConfirmBooking(IRestaurantNotifier restaurantNotifier)
            {
                // ..

                Confirmed = restaurantNotifier.NotifyBookingConfirmation(Restaurant, Customer, Id, BookingDetails);

                // ..
            }
        }
    }

    namespace DomainEventsAlternative
    {
        // Entity
        public class RestaurantBooking
        {
            // see entities chapter for options when injecting services
            private IRestaurantNotifier restaurantNotifier = null;

            public Guid Id { get; protected set; }

            public Restaurant Restaurant { get; protected set; }

            public Customer Customer { get; protected set; }

            public BookingDetails BookingDetails { get; protected set; }

            public bool Confirmed { get; protected set; }

            public void ConfirmBooking()
             {
                 // ..
         
                 DomainEvents.Raise(new BookingConfirmedByCustomer(this));
            
                 // ..
             }

            // ..
        }
    }

}
