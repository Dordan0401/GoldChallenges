using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldChallenges
{
    public class UI
    {
        MenuItemRepo _repo;
        public void Run()
        {
            Console.Clear();
            MenuItemRepo repo = new MenuItemRepo();
            _repo = repo;
            SeedMenu(repo);
            List<MenuItem> menu = repo.GetMenu();
            bool continueToRun = true;
            while (continueToRun == true)
            {
                Console.WriteLine("Welcome to MacBruhnalds Menu Management\n" +
                                  "----------------------------------------\n" +
                                  "1. View menu\n" +
                                  "2. Add new menu item\n" +
                                  "3. Delete a menu item\n" +
                                  "4. Exit\n" +
                                  "What would you like to do?\n");
                int userInput;
                if (!int.TryParse(Console.ReadLine(), out userInput))
                {
                    Console.WriteLine("Please enter a valid number");
                }

                switch (userInput)
                {
                    case 1:
                        WriteMenu(menu);
                        break;
                    case 2:
                        AddMenuItem(repo);
                        break;
                    case 3:
                        DeleteMenuItem(menu);
                        break;
                    case 4:
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please select an option that's on the menu");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        public List<MenuItem> SeedMenu(MenuItemRepo repo)
        {
            MenuItem fishSandy = new MenuItem(1, "Fish Sandwich Combo",
                                              "A crispy on the outside, juicy on the inside fish sandwich topped with crunchy lettuce and tartar sauce",
                                              5.99m);
            fishSandy.Ingredients = new List<string> { "Fish", "Tartar Sauce", "Lettuce", "Salt" };
            MenuItem baconBurger = new MenuItem(2, "Bacon Bruhburger Combo",
                                                "Our famous Bruhburger topped with bacon and fried onions",
                                                7.99m);
            baconBurger.Ingredients = new List<string> { "Beef", "Lettuce", "Tomato", "Sesame Bun", "Bacon", "Mayonnaise", "Ketchup", "Fried Onion" };
            repo.AddMenuItem(fishSandy);
            repo.AddMenuItem(baconBurger);
            return repo.GetMenu();
        }

        public void WriteMenu(List<MenuItem> menu)
        {
            Console.Clear();
            foreach (MenuItem item in menu)
            {
                Console.WriteLine($"\n#{item.MealNumber}. {item.MealName}\n" +
                    $"{item.MealDescription}\n" +
                    $"${item.MealPrice}\n\n" +
                    "Ingredients: ");
                int count = 0;
                foreach (string ingredient in item.Ingredients)
                {
                    if (count == item.Ingredients.Count - 1)
                    {
                        Console.WriteLine($"{ingredient}.\n");
                    }
                    else
                    {
                    Console.Write($"{ingredient}, ");
                    }
                    count++;
                }
                Console.WriteLine("---------------------------------------------------------------------");
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
            Console.Clear();
        }

        public void AddMenuItem(MenuItemRepo repo)
        {
            Console.Clear();
            Console.WriteLine("What is the number for your new item?");
            int newNum;
            if (!int.TryParse(Console.ReadLine(), out newNum))
            {
                Console.WriteLine("Please enter a number! Press any key to continue");
                Console.ReadKey();
                AddMenuItem(repo);
            }

            foreach (MenuItem item in repo.GetMenu())
            {
                if (newNum == item.MealNumber)
                {
                    Console.WriteLine("I'm sorry, that number has already been assigned\n" +
                        "Press any key to continue");
                    Console.ReadKey();
                    AddMenuItem(repo);
                }
            }
            Console.WriteLine("Entry accepted! Press any key to continue");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("What is the name of the new meal?");
            string newName = Console.ReadLine();
            Console.WriteLine("Entry accepted! Press any key to continue");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Please enter a description for your new meal");
            string newDesc = Console.ReadLine();
            Console.WriteLine("Entry accepted! Press any key to continue");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Please enter the price of your new meal");
            decimal newPrice;
            if (!decimal.TryParse(Console.ReadLine(), out newPrice))
            {
                Console.WriteLine("That's not a valid number.");
                AddMenuItem(repo);
            }
            Console.WriteLine("Entry accepted! Press any key to continue");
            Console.ReadKey();

            MenuItem newItem = new MenuItem(newNum, newName, newDesc, newPrice);

            Console.Clear();
            newItem.Ingredients = newItem.GetIngredients();
            Console.WriteLine("You're all finished! Press any key to return to the main menu");
            Console.ReadKey();
            Console.Clear();

            _repo.AddMenuItem(newItem);
        }

        public void DeleteMenuItem(List<MenuItem> menu)
        {
            Console.Clear();
            Console.WriteLine("Enter the number of the meal you would like to delete, or 'done' to return");
            string userResponse = Console.ReadLine();
            int mealNum;

            if (userResponse.ToLower() == "done")
            {
                Console.Clear();
                return;
            }else if (!int.TryParse(userResponse, out mealNum))
            {
                Console.WriteLine("Please enter a valid number! Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                DeleteMenuItem(menu);
            }

            MenuItem theMeal = _repo.GetMealByNum(mealNum);
            if (theMeal != null)
            {
                Console.WriteLine($"Are you sure you would like to delete the {theMeal.MealName}? y/n");
                string userInput = Console.ReadLine();
                if (userInput == "y")
                {
                    _repo.DeleteMenuItem(theMeal);
                    Console.WriteLine("Item deleted successfully! Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();

                }
                else if (userInput == "n")
                {
                    Console.WriteLine("No worries! Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("We didn't recognize your input, y/n please! Press any key to continue.");
                    Console.ReadKey();
                    DeleteMenuItem(menu);
                }
            }
            else
            {
                Console.WriteLine("I'm sorry, but that number was not found. Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                DeleteMenuItem(menu);
            }
        }
    }
}
