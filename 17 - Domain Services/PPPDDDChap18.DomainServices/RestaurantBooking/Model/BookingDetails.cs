using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.RestaurantBooking.Model
{
    // value object
    public class BookingDetails
    {
        public DateTime When { get; private set; }

        public int NumberOfDiners { get; private set; }

        // ...
    }
}
