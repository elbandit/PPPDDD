using System;

namespace DDDPPP.Chap21.NHibernateExample.Application.Infrastructure
{
   public class SystemClock : IClock
    {
        public DateTime Time()
        {
            return DateTime.Now;
        }
    }
}
