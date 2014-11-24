using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    namespace WithSideEffect
    {
        public class Dice
        {
            private Random r = new Random();

            public Dice(Guid id)
            {
                this.Id = id;
            }

            public Guid Id { get; private set; }

            // Bad: looks like a query, but changes every time
            public int Value()
            {
                return r.Next(1, 7);
            }

            // ..
        }
    }

    namespace SideEffectFree
    {
        public class Dice
        {
            private Random r = new Random();

            public Dice(Guid id)
            {
                this.Id = id;
            }

            public Guid Id { get; private set; }

            // Good: does not change each time called
            public int Value { get; private set; }

            // Good: sounds like a command - side-effect expected
            public void Roll()
            {
                Value = r.Next(1, 7);
            }
        }
    }
}
