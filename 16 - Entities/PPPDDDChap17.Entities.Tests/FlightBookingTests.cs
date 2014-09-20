using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap17.Entities.Examples;

namespace PPPDDDChap17.Entities.Tests
{
    [TestClass]
    public class FlightBookingTests
    {
        [TestMethod]
        public void Departure_date_can_be_rescheduled_whilst_pending_confirmation_from_airline()
        {
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var initialDeparture = new DateTime(2015, 04, 22);
            var booking = new FlightBooking(id, initialDeparture, customerId);

            var rescheduledDeparture = new DateTime(2015, 04, 23);
            booking.Reschedule(rescheduledDeparture);

            Assert.AreEqual(rescheduledDeparture, booking.DepartureDate);
        }

        [TestMethod]
        public void Departure_date_cannot_be_rescheduled_after_booking_confirmed_by_airline()
        {
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var initialDeparture = new DateTime(2015, 04, 22);
            var booking = new FlightBooking(id, initialDeparture, customerId);

            booking.Confirm();

            var rescheduledDeparture = new DateTime(2015, 04, 23);

            try
            {
                booking.Reschedule(rescheduledDeparture);
            }
            catch (RescheduleRejected rr)
            {
                // Exception was thrown so test should pass
                return;
            }

            Assert.Fail("Reschedule was not rejected");
        }
    }
}
