using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Examples;

namespace Tests
{
    [TestClass]
    public class Building_height_equality_tests
    {
        [TestMethod]
        public void Same_size_and_same_unit_are_equal_even_if_different_references()
        {
            var oneMetre = new BuildingHeight(1, MeasurmentUnit.Metres);
            var oneMetreX = new BuildingHeight(1, MeasurmentUnit.Metres);
            Assert.AreEqual(oneMetre, oneMetreX);

            var fiftyFeet = new BuildingHeight(50, MeasurmentUnit.Feet);
            var fiftyFeetX = new BuildingHeight(50, MeasurmentUnit.Feet);
            Assert.AreEqual(fiftyFeet, fiftyFeetX);
        }

        [TestMethod]
        public void Same_size_and_different_unit_are_not_equal()
        {
            var oneHundredMetres = new BuildingHeight(100, MeasurmentUnit.Metres);
            var oneHundredFeet = new BuildingHeight(100, MeasurmentUnit.Feet);
            Assert.AreNotEqual(oneHundredMetres, oneHundredFeet);
        }

        [TestMethod]
        public void Equivalent_sizes_in_different_units_are_equal_even_if_different_references()
        {
            var fortyNineMetres = new BuildingHeight(49, MeasurmentUnit.Metres);
            var oneHundredAndSixtyOneFeet = new BuildingHeight(161, MeasurmentUnit.Feet);
            Assert.AreEqual(fortyNineMetres, oneHundredAndSixtyOneFeet);
        }
    }
}
