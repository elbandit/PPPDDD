using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap17.Entities.Examples;

namespace PPPDDDChap17.Entities.Tests
{
    [TestClass]
    public class HotelTests
    {
        [TestMethod]
        public void Hotels_must_have_rooms_else_they_are_not_hotels()
        {
            var id = Guid.NewGuid();
            var availability = new HotelAvailability(null, null, null);
            
            try
            {
                var rooms = new HotelRoomSummary(0, 0, 0);
                new Hotel(id, availability, rooms);
            }
            catch (HotelsMustHaveRooms hmr)
            {
                // if exception is thrown test passes
                return;
            }

            Assert.Fail("Hotels must have rooms invariant not enforced");
            
        }
    }
}
