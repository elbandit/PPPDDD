using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.AmbiguousPhoneCall
{
    public class PhoneCall
    {
        public PhoneCall(String numberDialled, DateTime callStart, int callLengthInMinutes)
        {
            NumberDialled = numberDialled;
            Minutes = callLengthInMinutes;
            StartTime = callStart;
        }

        public DateTime StartTime { get; private set; }

        public int Minutes { get; private set; }

        public String NumberDialled { get; private set; }

    }
}

namespace Examples.ExplicitPhoneCall
{
    public class PhoneCall
    {
        public PhoneCall(PhoneNumber numberDialled, DateTime callStart, Minutes callLength)
        {
            NumberDialled = numberDialled;
            Minutes = callLength;
            StartTime = callStart;
        }

        public DateTime StartTime { get; private set; }

        public Minutes Minutes { get; private set; }

        public PhoneNumber NumberDialled { get; private set; }
    }

    // Value Objects 
    public class PhoneNumber
    {
        // ..
    }

    public class Minutes
    {
        // ..
    }
}
