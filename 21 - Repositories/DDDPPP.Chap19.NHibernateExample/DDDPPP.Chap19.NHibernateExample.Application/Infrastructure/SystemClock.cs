using System;

namespace DDDPPP.Chap19.NHibernateExample.Application.Infrastructure
{
   public class SystemClock : IClock
    {
        public DateTime Time()
        {
            return DateTime.Now;
        }
    }
}
