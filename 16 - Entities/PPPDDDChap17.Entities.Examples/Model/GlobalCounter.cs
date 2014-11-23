using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    public static class RandomEntityFactory
    {
        private static long lastId = 0;

        public static RandomEntity CreateEntity()
        {
            return new RandomEntity(++lastId);
        }
    }

    public class RandomEntity
    {
        public RandomEntity(long Id)
        {
            this.Id = Id;
        }

        public long Id { get; private set; }
    }
}
