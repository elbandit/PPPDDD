using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    // Entity
    public class BankAccount
    {
        public BankAccount(Guid id, Money startingBalance)
        {
            this.Id = id;
            this.Balance = startingBalance;
        }

        public Guid Id { get; private set; }

        public Money Balance { get; private set; }

        // ..
    }

    // Value Object
    public class Money
    {
        public Money(int amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        private int Amount { get; set; }

        private Currency Currency { get; set; }

        // ..
    }

    public enum Currency 
    {
        Dollars,
        Pounds,
    }
}
