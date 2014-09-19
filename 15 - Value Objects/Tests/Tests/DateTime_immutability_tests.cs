using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class DateTime_immutability_tests
    {
        [TestMethod]
        public void AddMonths_creates_new_immutable_DateTime()
        {
            var jan1st = new DateTime(2014, 01, 01);
            var feb1st = jan1st.AddMonths(1);

            // first object remains unchanged
            Assert.AreEqual(new DateTime(2014, 01, 01), jan1st);

            // second object is a new immutable instance
            Assert.AreEqual(new DateTime(2014, 02, 01), feb1st);
        }

        [TestMethod]
        public void AddYears_creates_new_immutable_DateTime()
        {
            var jan2014 = new DateTime(2014, 01, 01);
            var jan2015 = jan2014.AddYears(1);
            var jan2016 = jan2015.AddYears(1);

            // first object remains unchanged
            Assert.AreEqual(new DateTime(2014, 01, 01), jan2014);

            // second object remains unchanged
            Assert.AreEqual(new DateTime(2015, 01, 01), jan2015);

            Assert.AreEqual(new DateTime(2016, 01, 01), jan2016);
        }
    }
}
