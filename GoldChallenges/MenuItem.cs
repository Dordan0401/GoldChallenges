using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldChallenges
{
    public class MenuItem
    {
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public List<string> Ingredients { get; set; }
        public decimal MealPrice { get; set; }

        public MenuItem(int mealNum, string mealName, string mealDesc, decimal mealPrice)
        {
            MealNumber = mealNum;
            MealName = mealName;
            MealDescription = mealDesc;
            MealPrice = mealPrice;
        }

        public List<string> GetIngredients()
        {
            List<string> ingredientList = new List<string>();
            Console.WriteLine("List what ingredients are in this item one at a time\n" +
                "Then type 'Done' when you are finished");
            string mealIngredients = Console.ReadLine();
            while (!mealIngredients.ToLower().Contains("done"))
            {
                Console.Clear();
                ingredientList.Add(mealIngredients);
                Console.WriteLine("Ingredient added! Go ahead and type the next, or type 'Done'");
                mealIngredients = Console.ReadLine();
            }
            Console.Clear();
            return Ingredients = ingredientList;
        }
    }
}
