using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.BankAccount
{
    // Entity
    public class BankAccount
    {
        public BankAccount(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }

        public Money Balance { get; private set; }

        // ...
    }
}
