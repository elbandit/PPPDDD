using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap17.Entities.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Tests
{
    [TestClass]
    public class HolidayBookingTest
    {
        [TestMethod]
        public void Id_is_an_amalgamation_of_travelerId_and_dates()
        {
            var travelerId = 44563;
            var firstNight = new DateTime(2014, 07, 01);
            var lastNight = new DateTime(2014, 07, 14);
            var booked = new DateTime(2013, 09, 24);

            var booking = new HolidayBooking(travelerId, firstNight, lastNight, booked);

            var idForHoliday = "44563-2014/07/01-2014/07/14-2013/09/24";
            Assert.AreEqual(idForHoliday, booking.Id);
        }
    }
}
