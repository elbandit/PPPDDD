using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPPP.Chap21.EFExample.Application.Infrastructure
{
    public interface IClock
    {
        DateTime Time();
    }
}
