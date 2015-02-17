using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap05.TransactionScript.Domain
{
    public interface ICommand
    {
        public void Execute();
    }
}
