using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneNumber
    {
        public PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public string Number { get; set; }

        public bool IsUKLandlineOrMobile()
        {
            return Number.StartsWith("+44");
        }

        public bool IsInternational()
        {
            return !IsUKLandlineOrMobile();
        }
    }
}
