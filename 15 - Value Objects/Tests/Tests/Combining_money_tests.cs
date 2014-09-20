using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examples.CombiningMoney;

namespace Tests
{
    [TestClass]
    public class Combining_money_tests
    {
        [TestMethod]
        public void Money_supports_native_addition_syntax()
        {
            var m = new Money(200);
            var m2 = new Money(300);

            var combined = m + m2;

            Assert.AreEqual(new Money(500), combined);
        }

        [TestMethod]
        public void Money_supports_native_subtraction_syntax()
        {
            var m = new Money(50);
            var m2 = new Money(49);

            var combined = m - m2;

            Assert.AreEqual(new Money(1), combined);
        }
    }
}
