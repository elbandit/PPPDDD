using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    // Validating new departure date would occur prior to Reschedule() being called

    public class FlightBooking
    {
        private bool confirmed = false;

        public FlightBooking(Guid id, DateTime departureDate, Guid customerId)
        {
            if (id == null)
                throw new IdMissing();

            if (departureDate == null)
                throw new DepartureDateMissing();

            if (customerId == null)
                throw new CustomerIdMissing();

            this.Id = id;
            this.DepartureDate = departureDate;
            this.CustomerId = customerId;
        }

        public Guid Id { get; private set; }

        public DateTime DepartureDate { get; private set; }

        public Guid CustomerId { get; private set; }

        public void Reschedule(DateTime newDeparture)
        {
            if (confirmed) throw new RescheduleRejected();

            this.DepartureDate = newDeparture;
        }

        public void Confirm()
        {
            this.confirmed = true;
        }
    }

    public class IdMissing : Exception {}

    public class DepartureDateMissing : Exception { }

    public class CustomerIdMissing : Exception { }

    public class RescheduleRejected : Exception { }

    namespace ValidationWithSpecifications
    {
        public class FlightBooking
        {
            Specification<FlightBooking> ffSpec = new FrequentFlyersCanRescheduleAfterBookingConfirmation();
            Specification<FlightBooking> ndSpec = new NoDepartureReschedulingAfterBookingConfirmation();
            Specification<FlightBooking> spec;

            public FlightBooking(Guid Id, DateTime departureDate, Guid customerId, CustomerStatus customerStatus)
            {
                this.Id = Id;
                this.DepartureDate = departureDate;
                this.CustomerId = customerId;
                this.CustomerStatus = customerStatus;
                this.Confirmed = false;
                spec = ffSpec.Or(ndSpec);
            }

            public Guid Id { get; private set; }

            public DateTime DepartureDate { get; private set; }

            public Guid CustomerId { get; private set; }

            public bool Confirmed { get; private set; }

            public CustomerStatus CustomerStatus { get; private set; }

            public void Reschedule(DateTime newDeparture)
            {
                if (!spec.IsSatisfiedBy(this)) throw new RescheduleRejected();

                this.DepartureDate = newDeparture;
            }

            public void Confirm()
            {
                this.Confirmed = true;
            }
        }

        public class NoDepartureReschedulingAfterBookingConfirmation : Specification<FlightBooking>
        {
            public override bool IsSatisfiedBy(FlightBooking booking)
            {
                return !booking.Confirmed;
            }
        }

        public class FrequentFlyersCanRescheduleAfterBookingConfirmation : Specification<FlightBooking>
        {
            public override bool IsSatisfiedBy(FlightBooking booking)
            {
                return booking.CustomerStatus == CustomerStatus.FrequentFlyer
                    || booking.CustomerStatus == CustomerStatus.Gold;
            }
        }

        public class OrSpecification<T> : Specification<T>
        {
            private Specification<T> first;
            private Specification<T> second;

            public OrSpecification(Specification<T> first, Specification<T> second)
            {
                this.first = first;
                this.second = second;
            }

            public override bool IsSatisfiedBy(T entity)
            {
                return first.IsSatisfiedBy(entity) || second.IsSatisfiedBy(entity);
            }
        }

        public abstract class Specification<T> 
        {
            public Specification<T> Or(Specification<T> specification)
            {
                return new OrSpecification<T>(this, specification);
            }

            public abstract bool IsSatisfiedBy(T entity);

        }

        public enum CustomerStatus
        {
            Regular,
            FrequentFlyer,
            Gold
        }
    }
}
