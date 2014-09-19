using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameWithPersistence;

namespace Tests
{
    [TestClass]
    public class Persistence_format_examples
    {
        [TestMethod]
        public void Each_value_has_a_unique_representation()
        {
            var sallySmith = new Name("Sally", "Smith");
            Assert.AreEqual("firstName:Sally;;surname:Smith", sallySmith.ToString());

            var billyJean = new Name("Billy", "Jean");
            Assert.AreEqual("firstName:Billy;;surname:Jean", billyJean.ToString());
        }
    }
}
