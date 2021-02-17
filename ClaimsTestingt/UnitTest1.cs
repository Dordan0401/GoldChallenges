using KomodoClaims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ClaimsTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Claims> claimList = new List<Claims>();
            ClaimsRepo test = new ClaimsRepo(claimList);
            Claims claim = new Claims(ClaimType.car, "Big accident", 4500, new DateTime(2020, 3, 15), true);

            bool wasAdded = test.CreateClaim(claim);
            Assert.IsTrue(wasAdded);

            List<Claims> claimTest = test.GetClaims();
            List<Claims> listCheck = new List<Claims>() { claim };
            Assert.AreEqual(listCheck.Count, claimTest.Count);

            Claims idTest = test.GetClaimByID(1);
            Assert.AreEqual(claim, idTest);

            bool wasRemoved = test.DeleteClaim(claim);
            Assert.IsTrue(wasRemoved);
        }
    }
}
