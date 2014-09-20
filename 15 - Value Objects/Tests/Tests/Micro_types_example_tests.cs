using Examples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class Micro_types_example_tests
    {
        [TestMethod]
        public void Calculates_overtime_hours_as_hours_additional_to_contracted()
        {
            var hoursWorked = new Hours(40);
            var contractedHours = new Hours(35);

            // wrap with Micro Types for contextual explicitness
            var hoursWorkedx = new HoursWorked(hoursWorked);
            var contractedHoursx = new ContractedHours(contractedHours);

            var fiveHours = new Hours(5);
            var fiveHoursOvertime = new OvertimeHours(fiveHours);

            var result = new OvertimeCalculator().Calculate(hoursWorkedx, contractedHoursx);
            Assert.AreEqual(fiveHoursOvertime, result);
        }
    }
}
