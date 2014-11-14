using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Examples;

namespace Tests
{
    [TestClass]
    public class Meters_equality_tests
    {
        [TestMethod]
        public void Same_distances_are_equal_even_if_different_references()
        {
            var oneMeter = new Meters((decimal)1);
            var oneMeterX = new Meters((decimal)1);
            Assert.AreEqual(oneMeter, oneMeterX);

            var fiftyPoint25 = new Meters((decimal)50.25);
            var fiftyPoint25X = new Meters((decimal)50.25);
            Assert.AreEqual(fiftyPoint25, fiftyPoint25X);
        }

        [TestMethod]
        public void Same_distances_are_equal_even_if_different_references_with_base()
        {
            var oneMeter = new Examples.MetersWithBaseClass.Meters((decimal)11.22);
            var oneMeterX = new Examples.MetersWithBaseClass.Meters((decimal)11.22);
            Assert.AreEqual(oneMeter, oneMeterX);

            var fiftyMeters = new Examples.MetersWithBaseClass.Meters(50);
            var fiftyMetersX = new Examples.MetersWithBaseClass.Meters(50);
            Assert.AreEqual(fiftyMeters, fiftyMetersX);
        }
    }
}
