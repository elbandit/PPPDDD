using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap17.Entities.Examples;

namespace PPPDDDChap17.Entities.Tests
{
    [TestClass]
    public class GlobalCounterTest
    {
        [TestMethod]
        public void Each_new_entity_gets_the_next_sequential_Id()
        {
            var entity1 = RandomEntityFactory.CreateEntity();
            var entity2 = RandomEntityFactory.CreateEntity();
            var entity3 = RandomEntityFactory.CreateEntity();

            Assert.AreEqual(1, entity1.Id);
            Assert.AreEqual(2, entity2.Id);
            Assert.AreEqual(3, entity3.Id);
        }
    }
}
