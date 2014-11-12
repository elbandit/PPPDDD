using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.AmbiguousPhoneCall
{
    public class PhoneCall
    {
        public PhoneCall(String numberDialed, DateTime callStart, int callLengthInMinutes)
        {
            NumberDialed = numberDialed;
            Minutes = callLengthInMinutes;
            StartTime = callStart;
        }

        public DateTime StartTime { get; private set; }

        public int Minutes { get; private set; }

        public String NumberDialed { get; private set; }

    }
}

namespace Examples.ExplicitPhoneCall
{
    public class PhoneCall
    {
        public PhoneCall(PhoneNumber numberDialed, DateTime callStart, Minutes callLength)
        {
            NumberDialed = numberDialed;
            Minutes = callLength;
            StartTime = callStart;
        }

        public DateTime StartTime { get; private set; }

        public Minutes Minutes { get; private set; }

        public PhoneNumber NumberDialed { get; private set; }
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
