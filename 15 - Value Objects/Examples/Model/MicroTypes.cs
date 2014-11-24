using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    // Domain service that operates on Value Objects
    public class OvertimeCalculator
    {
        public OvertimeHours Calculate(HoursWorked worked, ContractedHours contracted)
        {
            var overtimeHours = worked.Hours - contracted.Hours;
            return new OvertimeHours(overtimeHours);
        }
    }

    public class Hours : ValueObject<Hours>
    {
        public readonly int Amount;

        public Hours(int amount)
        {
            this.Amount = amount;
        }

        public static Hours operator - (Hours left, Hours right)
        {
            return new Hours(left.Amount - right.Amount);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { Amount };
        }
    }

    // Micro Types that wrap existing types for contextual clarity
    public class HoursWorked : ValueObject<HoursWorked>
    {
        public readonly Hours Hours;

        public HoursWorked(Hours hours)
        {
            this.Hours = hours;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { Hours };
        }
    }

    public class ContractedHours : ValueObject<ContractedHours>
    {
        public readonly Hours Hours;

        public ContractedHours(Hours hours)
        {
            this.Hours = hours;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { Hours };
        }
    }

    public class OvertimeHours : ValueObject<OvertimeHours>
    {
        public readonly Hours Hours;

        public OvertimeHours(Hours hours)
        {
            this.Hours = hours;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] { Hours };
        }
    }
}
