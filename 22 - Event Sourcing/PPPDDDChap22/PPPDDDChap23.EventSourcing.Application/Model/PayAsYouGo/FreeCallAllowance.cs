using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class FreeCallAllowance
    {
        // Your allowances will expire after 30 days.  
        // Calls to standard UK mobiles and landlines (01, 02, 03) within the UK.

        public Minutes Allowance { get; private set; }
        public DateTime DateStarted { get; private set; }

        public FreeCallAllowance(Minutes allowance, DateTime dateStarted)
        {
            Allowance = allowance;
            DateStarted = dateStarted;
        }

        public void Subtract(Minutes minutes)
        {
            Allowance = Allowance.Subtract(minutes);
        }
        
        public Minutes MinutesWhichCanCover(PhoneCall phoneCall, IClock clock)
        {
            if (Allowance.IsGreaterOrEqualTo(phoneCall.Minutes))
            {
                return phoneCall.Minutes;
            }
            else
            {
                return Allowance;
            }
        }

        private bool StillValid(IClock clock)
        {
            return DateStarted.AddDays(30) > clock.Time();
        }
    }
}
