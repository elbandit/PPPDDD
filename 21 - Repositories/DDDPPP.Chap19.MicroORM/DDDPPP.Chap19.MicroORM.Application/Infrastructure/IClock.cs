using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPPP.Chap19.MicroORM.Application.Infrastructure
{
    public interface IClock
    {
        DateTime Time();
    }
}
