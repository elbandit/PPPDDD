using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examples;

namespace Tests
{
    [TestClass]
    public class Height_with_base_equality_tests
    {
        [TestMethod]
        public void Same_size_and_same_unit_are_equal_even_if_different_references()
        {
            var oneMetre = new HeightWithBase(1, MeasurmentUnit.Meters);
            var oneMetreX = new HeightWithBase(1, MeasurmentUnit.Meters);
            var areEqual = oneMetre == oneMetreX; 
            Assert.IsTrue(areEqual);

            var fiftyFeet = new HeightWithBase(50, MeasurmentUnit.Feet);
            var fiftyFeetX = new HeightWithBase(50, MeasurmentUnit.Feet);
            var areEqual2 = fiftyFeet.Equals(fiftyFeetX);
            Assert.IsTrue(areEqual2);
        }

        [TestMethod]
        public void Same_size_and_different_unit_are_not_equal()
        {
            var oneHundredMetres = new HeightWithBase(100, MeasurmentUnit.Meters);
            var oneHundredFeet = new HeightWithBase(100, MeasurmentUnit.Feet);
            var areNotEqual = oneHundredMetres != oneHundredFeet;
            Assert.IsTrue(areNotEqual);
        }

        [TestMethod]
        public void Equivalent_sizes_in_different_units_are_equal_even_if_different_references()
        {
            var fortyNineMetres = new HeightWithBase(49, MeasurmentUnit.Meters);
            var oneHundredAndSixtyOneFeet = new HeightWithBase(161, MeasurmentUnit.Feet);
            Assert.AreEqual(fortyNineMetres, oneHundredAndSixtyOneFeet);
        }
    }
}
