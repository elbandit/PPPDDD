using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TimeSpan_factory_method_tests
    {
        [TestMethod]
        public void TimeSpan_factory_methods()
        {
            var sixDays = TimeSpan.FromDays(6);
            var threeHours = TimeSpan.FromHours(3);
            var twoMillis = TimeSpan.FromMilliseconds(2);

            var sixDaysx = new TimeSpan(6, 0, 0, 0, 0);
            var threeHoursx = new TimeSpan(0, 3, 0, 0, 0);
            var twoMillisx = new TimeSpan(0, 0, 0, 0, 2);

            Assert.AreEqual(sixDays, sixDaysx);
            Assert.AreEqual(threeHours, threeHoursx);
            Assert.AreEqual(twoMillis, twoMillisx);
        }
    }
}
