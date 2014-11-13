using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    // Value Object
    public class Height
    {
        public Height(int size, MeasurmentUnit unit)
        {
            this.Size = size;
            this.Unit = unit;
        }

        public int Size { get; private set; }

        public MeasurmentUnit Unit { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as Height;

            if (other == null) return false;

            if (other.Unit == this.Unit)
            {
                return other.Size == this.Size;
            }

            // if we get here, we know they are different units
            if (other.Unit == MeasurmentUnit.Feet)
            {
                return GetAsFeet() == other.Size;
            }

            if (other.Unit == MeasurmentUnit.Meters)
            {
                return GetAsMeters() == other.Size;
            }

            throw new Exception("Unexpected measurement unit");
        }

        private int GetAsFeet()
        {
            // Expects current size to be in meters so converts from metres to feet
            return (int)Math.Round(this.Size * 3.2808399, MidpointRounding.AwayFromZero);
        }

        private int GetAsMeters()
        {
            // Expects current size to be in feet, so converts from feet to meters
            return (int)Math.Round(this.Size * 0.3048, MidpointRounding.AwayFromZero);
        }

        public static Height FromFeet(int feet)
        {
            return new Height(feet, MeasurmentUnit.Feet);
        }

        public static Height FromMeters(int metres)
        {
            return new Height(metres, MeasurmentUnit.Meters);
        }
    }

    public enum MeasurmentUnit
    {
        Feet,
        Meters
    }
}
