using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    // Entity
    public class Book
    {
        public Book(ISBN isbn)
        {
            this.ISBN = isbn;
            this.Id = isbn.Number;
        }

        public string Id { get; private set; }

        public ISBN ISBN { get; private set; }
    }

    // Simplified Value Object - see chapter 16 for robust implementations
    public class ISBN
    {
        public ISBN(string isbn)
        {
            Validate(isbn);

            this.Number = isbn;
        }

        public string Number { get; set; }

        private void Validate(string isbn)
        {
            if (isbn.Length != 13 && isbn.Length != 10)
                throw new Exception("ISBN must be 10  or 13 digits long");

            int outValue = 0;
            foreach (char c in isbn)
            {
                if (!int.TryParse(c.ToString(), out outValue))
                    throw new Exception("ISBNs must contain only digits");
            }
        }
    }
}
