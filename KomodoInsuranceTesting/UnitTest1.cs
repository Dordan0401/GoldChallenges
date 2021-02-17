using KomodoInsurance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KomodoInsuranceTesting
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            BadgeRepo testRepo = new BadgeRepo();
            Dictionary<int, Badge> testBadges = testRepo.GetBadges();
            List<string> testDoors = new List<string>() { "A24", "B3" };
            Badge testBadge = new Badge(testDoors, 12345);

            bool wasAdded = testRepo.CreateBadge(testBadge);
            Assert.IsTrue(wasAdded);

            bool isPopulated = testBadges.Count > 0;
            Assert.IsTrue(isPopulated);

            bool wasUpdated = testRepo.UpdateBadge(12345, new Badge(new List<string> { "A3" }, 12345));
            Assert.IsTrue(wasUpdated);

            bool wasRemoved = testRepo.DeleteBadge(testBadge);
            Assert.IsTrue(wasRemoved);
        }
    }
}
