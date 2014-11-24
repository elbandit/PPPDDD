using PPPDDDChap18.DomainServices.Infrastructure;
using PPPDDDChap18.DomainServices.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices
{
    // Possible Solutions:

    /* 
    public static class RestaurantBookingFactory
    {
        // Initial object - no ORM to worry about here
        // but will this approach be compatible with an ORM?
        public RestaurantBooking CreateBooking(Restaurant restaurant, Customer customer, BookingDetails details)
        {
            var id = Guid.NewGuid();
            var notifier = new RestaurantNotifier();
            return new RestaurantBooking(restaurant, customer, details, id, notifier);
        }
    }
    */

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



    // Entity
    public class RestaurantBooking
    {
        // see entities chapter for options when injecting services
        private IRestaurantNotifier restaurantNotifier = null;

        public Guid Id { get; protected set; }

        public Restaurant Restaurant { get; protected set; }

        public Customer Customer { get; protected set; }

        public BookingDetails BookingDetails { get; protected set;}

        public bool Confirmed { get; protected set; }

        // ..

        // Entity behavior that depends on a Domain Service (IB
        public void ConfirmBooking()
        {
            // ..

            // restaurantNotifier is the Domain Service
            // but how can it be available in this scope? especially when an ORM is involved?
            Confirmed = restaurantNotifier.NotifyBookingConfirmation(Restaurant, Customer, Id, BookingDetails);

            // ..
        }

        /* 
        // Double dispatch
        public void ConfirmBooking(IRestaurantNotifier restaurantNotifier)
        {
            // ..
         
            Confirmed = restaurantNotifier.NotifyBookingConfirmation(Restaurant, Customer, Id, BookingDetails);
         
            // ..
        }
        */

        /*
         // Domain Events
         public void ConfirmBooking()
         {
             // ..
         
             DomainEvents.Raise(new BookingConfirmedByCustomer>(this));
            
             // ..
         }
         */

        // ..
    }

    // Entity
    public class Restaurant
    {
        public Guid Id { get; protected set; }

        // ..
    }

    // Entity
    public class Customer
    {
        public Guid Id { get; protected set; }

        // ..
    }

    // Value Object
    public class BookingDetails
    {
        public DateTime When { get; private set; }

        public int NumberOfDiners { get; private set; }

        // ...
    }

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
