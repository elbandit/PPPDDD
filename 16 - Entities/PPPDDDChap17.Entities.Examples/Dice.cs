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

            public int LastValue { get; private set; }

            public void Roll()
            {
                LastValue = r.Next(1, 7);
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

            public int Roll()
            {
                return r.Next(1, 7);
            }

            // ..
        }
    }
}
