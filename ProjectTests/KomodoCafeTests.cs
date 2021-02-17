using GoldChallenges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectTests
{
    [TestClass]
    public class KomodoCafeTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            MenuItemRepo repo = new MenuItemRepo();
            MenuItem fishSandy = new MenuItem(1, "Fish Sandwich", "A crispy on the outside, juicy on the inside fish sandwich topped with crunchy lettuce and tartar sauce",
                4.99m);
            fishSandy.GetIngredients();
            MenuItem anotherSandy = new MenuItem(2, "Sandwich", "A good ole sandwich", 5.99m);
            repo.AddMenuItem(fishSandy);
            repo.AddMenuItem(anotherSandy);
            repo.GetMenu();
        }
    }
}
