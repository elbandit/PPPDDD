using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneCall
    {
        public PhoneCall(PhoneNumber numberDialled, DateTime callStart, Minutes callLength)
        {
            NumberDialled = numberDialled;
            Minutes = callLength;
            StartTime = callStart;
        }

        public DateTime StartTime {get; private set;} 
        public Minutes Minutes {get; private set;}
        public PhoneNumber NumberDialled { get; private set; }
    }
}
