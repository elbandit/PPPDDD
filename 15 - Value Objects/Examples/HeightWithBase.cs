using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public class HeightWithBase : ValueObject<HeightWithBase>
    {
        public HeightWithBase(int size, MeasurmentUnit unit)
        {
            this.Size = size;
            this.Unit = unit;
        }

        public int Size { get; private set; }

        public MeasurmentUnit Unit { get; private set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            // Convert all objects into same unit of measurement for true equality checks
            // In some domains equality may be based on the unit of measurement also being the same
            return new object[] { GetAsFeet() };
        }

        private int GetAsFeet()
        {
            // In this domain decimal precision is massively insignficant so rounding to nearest unit is good enough

            if (Unit == MeasurmentUnit.Feet) return Size; // already in feet

            return (int)Math.Round(this.Size * 3.2808399, MidpointRounding.AwayFromZero);
        }
    }
}
