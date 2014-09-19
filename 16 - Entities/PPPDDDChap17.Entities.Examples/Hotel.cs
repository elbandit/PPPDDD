using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    public class Hotel
    {
        public Hotel(Guid id, HotelAvailability initialAvailability, HotelRoomSummary rooms)
        {
            EnforceInvariants(rooms);
            this.Id = id;
            this.Availability = initialAvailability;
            this.Rooms = rooms;
        }

        private void EnforceInvariants(HotelRoomSummary rooms)
        {
            if (rooms.NumberOfSingleRooms < 1 && 
                rooms.NumberOfDoubleRooms < 1 && 
                rooms.NumberOfFamilyRooms < 1)
                throw new HotelsMustHaveRooms();
        }

        public Guid Id { get; private set; }

        public HotelAvailability Availability { get; private set; }

        public HotelRoomSummary Rooms { get; private set; }
        
        // Other responsiblities: facilities, events, staff details
    }

    // Value Object - see chapter 16 for full-feature Value Objects
    public class HotelAvailability
    {
        public HotelAvailability(RoomAvailability singleRooms, RoomAvailability doubleRooms,
            RoomAvailability FamilyRooms)
        {
            this.SingleRooms = singleRooms;
            this.DoubleRooms = doubleRooms;
            this.FamilyRooms = FamilyRooms;
        }

        // These are private so encapsulated. Only Hotel.Availability is accessible from outside the Entity
        private RoomAvailability SingleRooms { get; set; }

        private RoomAvailability DoubleRooms { get; set; }

        private RoomAvailability FamilyRooms { get; set; }

        public bool HasSingleRoomAvailability(DateTime start, DateTime end)
        {
            return GetSingleRoomAvailability(start, end) != null;
        }

        public bool HasDoubleRoomAvailability(DateTime start, DateTime end)
        {
            return GetDoubleRoomAvailability(start, end) != null;
        }

        public bool HasFamilyRoomAvailability(DateTime start, DateTime end)
        {
            return GetFamilyRoomAvailability(start, end) != null;
        }

        public Money GetSingleRoomPriceFor(DateTime start, DateTime end)
        {
            return GetSingleRoomAvailability(start, end).PricePerNight;
        }

        public Money GetDoubleRoomPriceFor(DateTime start, DateTime end)
        {
            return GetDoubleRoomAvailability(start, end).PricePerNight;
        }

        public Money GetFamilyRoomPriceFor(DateTime start, DateTime end)
        {
            return GetFamilyRoomAvailability(start, end).PricePerNight;
        }

        private AvailableBookingSlot GetSingleRoomAvailability(DateTime start, DateTime end)
        {
            return SingleRooms.Dates.SingleOrDefault(dr => dr.Start <= start && dr.End >= end);
        }

        private AvailableBookingSlot GetDoubleRoomAvailability(DateTime start, DateTime end)
        {
            return DoubleRooms.Dates.SingleOrDefault(dr => dr.Start <= start && dr.End >= end);
        }

        private AvailableBookingSlot GetFamilyRoomAvailability(DateTime start, DateTime end)
        {
            return FamilyRooms.Dates.SingleOrDefault(dr => dr.Start <= start && dr.End >= end);
        }

    }

    public class RoomAvailability
    {
        public IEnumerable<AvailableBookingSlot> Dates { get; private set; }
    }

    public class AvailableBookingSlot
    {
        public AvailableBookingSlot(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public Money PricePerNight { get; private set; }

        public int Rooms { get; private set; }
    }

    // See chapter 16 full a full Money Value Object implementation
    public class Money 
    {
        protected readonly decimal Value;

        public Money()
            : this(0m)
        {
        }

        public Money(decimal value)
        {
            Value = value;
        }
    }

    public class HotelRoomSummary
    {
        public HotelRoomSummary(int singleRooms, int doubleRooms, int familyRooms)
        {
            this.NumberOfSingleRooms = singleRooms;
            this.NumberOfDoubleRooms = doubleRooms;
            this.NumberOfFamilyRooms = familyRooms;
        }

        public int NumberOfSingleRooms { get; private set; }

        public int NumberOfDoubleRooms { get; private set; }

        public int NumberOfFamilyRooms { get; private set; }
    }

    public class HotelsMustHaveRooms : Exception { }
}

