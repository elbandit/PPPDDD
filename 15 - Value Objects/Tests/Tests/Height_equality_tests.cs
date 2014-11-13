using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Examples;

namespace Tests
{
    [TestClass]
    public class Height_equality_tests
    {
        [TestMethod]
        public void Same_size_and_same_unit_are_equal_even_if_different_references()
        {
            var oneMetre = new Height(1, MeasurmentUnit.Meters);
            var oneMetreX = new Height(1, MeasurmentUnit.Meters);
            Assert.AreEqual(oneMetre, oneMetreX);

            var fiftyFeet = new Height(50, MeasurmentUnit.Feet);
            var fiftyFeetX = new Height(50, MeasurmentUnit.Feet);
            Assert.AreEqual(fiftyFeet, fiftyFeetX);
        }

        [TestMethod]
        public void Same_size_and_different_unit_are_not_equal()
        {
            var oneHundredMetres = new Height(100, MeasurmentUnit.Meters);
            var oneHundredFeet = new Height(100, MeasurmentUnit.Feet);
            Assert.AreNotEqual(oneHundredMetres, oneHundredFeet);
        }

        [TestMethod]
        public void Equivalent_sizes_in_different_units_are_equal_even_if_different_references()
        {
            var fortyNineMetres = new Height(49, MeasurmentUnit.Meters);
            var oneHundredAndSixtyOneFeet = new Height(161, MeasurmentUnit.Feet);
            Assert.AreEqual(fortyNineMetres, oneHundredAndSixtyOneFeet);
        }
    }
}
