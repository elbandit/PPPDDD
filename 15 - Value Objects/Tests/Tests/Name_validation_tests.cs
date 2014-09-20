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
    public class Name_validation_tests
    {
        [TestMethod]
        public void First_names_cannot_be_empty()
        {
            try
            {
                var name = new Name("", "Torvalds");
            }
            catch (ApplicationException e)
            {
                Assert.AreEqual("You must specify a first name.", e.Message);
                return;
            }

            Assert.Fail("No ApplicationException was thrown");
        }

        [TestMethod]
        public void Surnames_cannot_be_empty()
        {
            try
            {
                var name = new Name("Linus", "");
            }
            catch (ApplicationException e)
            {
                Assert.AreEqual("You must specify a surname.", e.Message);
                return;
            }

            Assert.Fail("No ApplicationException was thrown");
        }
    }
}
