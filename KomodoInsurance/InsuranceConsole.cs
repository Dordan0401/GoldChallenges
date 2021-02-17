using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance
{
    public class InsuranceConsole
    {
        BadgeRepo _repo;
        Dictionary<int, Badge> _dictionary;
        public void Run()
        {
            _repo = new BadgeRepo();
            _dictionary = _repo.GetBadges();
            _repo.Seed();
            bool continueToRun = true;

            while (continueToRun)
            {
            Start:
                Console.Clear();
                Console.WriteLine("Welcome to Komodo Security management, admin\n" +
                                  "-----------------------------------------------\n" +
                                  "What would you like to do?\n\n" +
                                  "1. See all badges\n" +
                                  "2. Add a badge\n" +
                                  "3. Edit a badge\n" +
                                  "4. Exit");

                string userInput = Console.ReadLine();
                int convertedInput;
                if (!int.TryParse(userInput, out convertedInput))
                {
                    Console.WriteLine("Please input a number");
                    goto Start;
                }

                switch (convertedInput)
                {
                    case 1:
                        WriteBadges();
                        break;
                    case 2:
                        NewBadge();
                        break;
                    case 3:
                        EditBadge();
                        break;
                    case 4:
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please choose a valid menu option");
                        goto Start;
                }
            }
        }

        public void WriteBadges()
        {
            Console.Clear();
            Console.WriteLine($"{"Badge ID",-10} Doors\n" +
                $"-------------------------------------------");
            foreach (KeyValuePair<int, Badge> badge in _dictionary)
            {
                Console.Write($"{badge.Key,-11}| ");
                foreach (string door in badge.Value.DoorAccess)
                {
                    Console.Write($"{door} | ");
                }
                Console.WriteLine("\n-------------------------------------------");
            }
            Console.WriteLine("\nPress any key to go back");
            Console.ReadKey();
        }

        public void WriteSingleBadge(Badge writeThisBadge)
        {
            Console.WriteLine($"{"Badge ID",-10} Doors\n" +
            $"-------------------------------------------");
            Console.Write($"{writeThisBadge.BadgeID,-11}| ");
            foreach (string door in writeThisBadge.DoorAccess)
            {
                Console.Write($"{door} | ");
            }
            Console.WriteLine("\n-------------------------------------------");
        }

        public void NewBadge()
        {
        First:
            Console.Clear();
            Badge newBadge = new Badge();
            Console.WriteLine("Please enter the badge number");
            string userInput = Console.ReadLine();
            int convertedInput;
            if (!int.TryParse(userInput, out convertedInput))
            {
                Console.WriteLine("Please enter a number");
                Console.ReadKey();
                goto First;
            }
            if (!_dictionary.ContainsKey(convertedInput))
            {
                newBadge.BadgeID = convertedInput;
            }
            else
            {
                Console.WriteLine("I'm sorry, this badge ID is already in use");
                Console.ReadKey();
                goto First;
            }
            bool imDone = false;
            newBadge.DoorAccess = new List<string>();
            while (imDone == false)
            {
            Second:
                Console.Clear();
                Console.WriteLine("Enter a door you would like this badge to have access to");
                userInput = Console.ReadLine();
                newBadge.DoorAccess.Add(userInput);
                Console.WriteLine("\nAny others? y/n");
                userInput = Console.ReadLine();
                if (userInput.ToLower().Contains("yes") || userInput.ToLower().Contains("y"))
                {
                    goto Second;
                }
                else if (userInput.ToLower().Contains("no") || userInput.ToLower().Contains("n"))
                {
                    imDone = true;
                }
                else
                {
                    Console.WriteLine("I'm sorry, I can only understand yes or no");
                    Console.ReadKey();
                    goto Second;
                }
            }
            _repo.CreateBadge(newBadge);
            Console.Clear();
            Console.WriteLine("Badge successfully created!\n" +
                "Press any key to go back");
            Console.ReadKey();
        }

        public void EditBadge()
        {
        First:
            Console.Clear();
            Console.WriteLine("Enter the ID of the badge you'd like to edit");
            string userInput = Console.ReadLine();
            int convertedInput;
            if (!int.TryParse(userInput, out convertedInput))
            {
                Console.WriteLine("The badge ID must be a number");
                Console.ReadKey();
                goto First;
            }
            Badge editBadge = _repo.GetBadgeByID(convertedInput);
        Second:
            Console.Clear();
            WriteSingleBadge(editBadge);
            Console.WriteLine("\n What would you like to do with this badge?\n" +
                              "1. Update Badge\n" +
                              "2. Delete Badge");
            userInput = Console.ReadLine();
            if (!int.TryParse(userInput, out convertedInput))
            {
                Console.WriteLine("Please enter a number");
                Console.ReadKey();
                goto Second;
            }
            switch (convertedInput)
            {
                case 1:
                    bool imDone = false;
                    editBadge.DoorAccess = new List<string>();
                    while (imDone == false)
                    {
                    Third:
                        Console.Clear();
                        Console.WriteLine("Enter a door you would like this badge to have access to (Including ones it already does)");
                        userInput = Console.ReadLine();

                        editBadge.DoorAccess.Add(userInput);
                        Console.WriteLine("\nAny others? y/n");
                        userInput = Console.ReadLine();

                        if (userInput.ToLower().Contains("yes") || userInput.ToLower().Contains("y"))
                        {
                            goto Third;
                        }
                        else if (userInput.ToLower().Contains("no") || userInput.ToLower().Contains("n"))
                        {
                            imDone = true;
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, I can only understand yes or no");
                            Console.ReadKey();
                            goto Third;
                        }
                    }
                    _repo.UpdateBadge(editBadge);
                    Console.Clear();
                    Console.WriteLine("Badge updated successfully!\n" +
                        "Press any key to continue");
                    break;
                case 2:
                    _repo.DeleteBadge(editBadge);
                    Console.Clear();
                    Console.WriteLine("Badge deleted successfully!\n" +
                        "Press any key to continue");
                    break;
            }
            Console.ReadKey();
        }

    }
}
