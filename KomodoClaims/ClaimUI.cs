using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims
{
    public class ClaimUI
    {
        ClaimsRepo _repo;
        public void Run()
        {
            List<Claims> claimList = new List<Claims>();
            _repo = new ClaimsRepo(claimList);
            SeedClaims(claimList);
            bool continueToRun = true;
            while (continueToRun == true)
            {
                Start:
                Console.Clear();
                Console.WriteLine("Welcome to Komodo Claims management\n" +
                                  "-------------------------------------\n\n" +
                                  "1. See all claims\n" +
                                  "2. Take care of next claim in line\n" +
                                  "3. Take care of a specific claim\n" +
                                  "4. Enter a new claim\n" +
                                  "5. Exit\n\n" +
                                  "-------------------------------------\n" +
                                  "What would you like to do?");
                string userInput = Console.ReadLine();
                int convertedInput;
                if (!int.TryParse(userInput, out convertedInput))
                {
                    Console.WriteLine("Please enter a valid number.");
                    Console.ReadKey();
                    goto Start;
                }
                switch (convertedInput)
                {
                    case 1:
                        WriteAllClaims(claimList);
                        break;
                    case 2:
                        HandleNextClaim(claimList);
                        break;
                    case 3:
                        HandleClaimChoose(claimList);
                        break;
                    case 4:
                        EnterNewClaim(claimList);
                        break;
                    case 5:
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please choose a valid menu option");
                        Console.ReadKey();
                        goto Start;
                }
            }

        }

        // For writing all claims
        public void WriteAllClaims(List<Claims> claims)
        {
            Console.Clear();
            Console.WriteLine($"{"Claim ID",-10} {"Type",-10} {"Description",-30}" +
                              $"{"Amount",-15} {"Accident Date",-20} {"Claim Date",-20} {"Valid?",-10}\n\n" +
                              "---------------------------------------------------------------------------------------------------------------------\n");
            foreach (Claims claim in claims)
            {
                Console.WriteLine($"{claim.ClaimID,-10} {claim.ClaimType,-10} {claim.Description,-30}" +
                              $"{claim.ClaimAmount,-15} {claim.DateOfIncident.ToShortDateString(),-20} {claim.DateOfClaim.ToShortDateString(),-20} {claim.IsValid,-10}\n");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
        }

        // If the user chooses to, they can "handle" the first item in the claims list
        public void HandleNextClaim(List<Claims> claims)
        {
            Input:
            Console.Clear();
            if (claims.Count == 0)
            {
                Console.WriteLine("No more claims! Hooray!\n" +
                    "Press any key to go back");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Found the next claim\n");
            }
            WriteClaim(claims[0]);
            Console.WriteLine("\nWould you like to handle this claim now? y/n");
            string userInput = Console.ReadLine();
            switch (userInput.ToLower())
            {
                case "y":
                case "yes":
                case "yeah":
                    _repo.DeleteClaim(claims[0]);
                    Console.WriteLine("\nClaim handled successfully!\n" +
                        "Press any key to go back");
                    Console.ReadKey();
                    return;
                case "no":
                case "n":
                case "nah":
                    Console.WriteLine("\nNo worries!\n" +
                        "Just press any key to go back to the main menu");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("You can only say yes or no, idiot. Try again");
                    Console.ReadKey();
                    goto Input;
            }
            Console.ReadKey();

        }

        // User can handle a specific claim, if needed
        public void HandleClaimChoose(List<Claims> claims)
        {
            Input2:
            Console.Clear();
            Console.WriteLine("Enter the ID of the claim you'd like to handle");
            string userInput = Console.ReadLine();
            int convertedInput;
            if (!int.TryParse(userInput, out convertedInput))
            {
                Console.WriteLine("The ID is a number");
                Console.ReadKey();
                HandleClaimChoose(claims);
            }
            if (convertedInput <= claims.Count)
            {
                Console.Clear();
                WriteClaim(_repo.GetClaimByID(convertedInput));
                Console.WriteLine("\nAre you sure you would like to handle this claim now? y/n");
                string userInputDos = Console.ReadLine();
                switch (userInputDos.ToLower())
                {
                    case "y":
                    case "yes":
                    case "yeah":
                        _repo.DeleteClaim(claims[convertedInput]);
                        Console.WriteLine("\nClaim handled successfully!\n" +
                            "Press any key to go back");
                        Console.ReadKey();
                        return;
                    case "no":
                    case "n":
                    case "nah":
                        Console.WriteLine("\nNo worries!\n" +
                            "Just press any key to go back to the main menu");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("You can only say yes or no, idiot. Try again");
                        Console.ReadKey();
                        goto Input2;
                }
            }
            else
            {
                Console.WriteLine("I'm sorry, but that ID wasn't found");
                Console.ReadKey();
                goto Input2;
            }
        }

        // User can create a new claim
        public void EnterNewClaim(List<Claims> claims)
        {
            Console.Clear();
            int[] count = new int[] { 1, 2, 3, 4, 5 };
            bool isValid;
            string newDesc;
            ClaimType newType;
            decimal newAmt;
            DateTime newDate;
            Claims newClaim = new Claims();
            foreach (int num in count)
            {
                Console.Clear();
                switch (num)
                {
                    case 1:
                        Console.WriteLine("New Claim\n" +
                        "----------------------------");
                        Console.WriteLine("What type of claim is this?");
                        string userInput = Console.ReadLine();
                        if (!ClaimType.TryParse(userInput.ToLower(), out newType))
                        {
                            Console.WriteLine("That isn't a valid claim type (Car/Home/Theft)");
                            Console.ReadKey();
                            Console.Clear();
                            goto case 1;
                        }
                        newClaim.ClaimType = newType;
                        break;
                    case 2:
                        Console.WriteLine("New Claim\n" +
                        "----------------------------");
                        Console.WriteLine("Accepted! Enter a description for this claim");
                        newDesc = Console.ReadLine();
                        newClaim.Description = newDesc;
                        break;
                    case 3:
                        Console.WriteLine("New Claim\n" +
                        "----------------------------");
                        Console.WriteLine("Accepted! What is the amount of damage cost?");
                        userInput = Console.ReadLine();
                        if (!decimal.TryParse(userInput, out newAmt))
                        {
                            Console.WriteLine("It needs to be a number");
                            Console.ReadKey();
                            Console.Clear();
                            goto case 3;
                        }
                        newClaim.ClaimAmount = newAmt;
                        break;
                    case 4:
                        Console.WriteLine("New Claim\n" +
                        "----------------------------");
                        Console.WriteLine("Accepted! Please enter the date of the incedent 'yyyy, mm, dd'");
                        userInput = Console.ReadLine();
                        if (!DateTime.TryParse(userInput, out newDate))
                        {
                            Console.WriteLine("Please use the correct format 'yyyy, mm, dd'");
                            Console.ReadKey();
                            Console.Clear();
                            goto case 4;
                        }
                        newClaim.DateOfIncident = newDate;
                        break;
                    case 5:
                        Console.WriteLine("New Claim\n" +
                        "----------------------------");
                        Console.WriteLine("Accepted! Is this claim valid? y/n");
                        userInput = Console.ReadLine();
                        if (userInput.Contains("y"))
                        {
                            isValid = true;
                        }
                        else if (userInput.Contains("n"))
                        {
                            isValid = false;
                        }
                        else
                        {
                            Console.WriteLine("Yes or no, please");
                            Console.ReadKey();
                            Console.Clear();
                            goto case 5;
                        }
                        newClaim.IsValid = isValid;
                        break;
                }
            }
            _repo.CreateClaim(newClaim);
            Console.WriteLine("All entries finished!\n" +
                "Press any key to return to the main menu");
            Console.ReadKey();
        }

        // For writing a single claim
        public void WriteClaim(Claims claim)
        {
            Console.WriteLine($"{"Claim ID",-10} {"Type",-10} {"Description",-30}" +
                              $"{"Amount",-15} {"Accident Date",-20} {"Claim Date",-20} {"Valid?",-10}\n" +
                              "---------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine($"{claim.ClaimID,-10} {claim.ClaimType,-10} {claim.Description,-30}" +
                              $"{claim.ClaimAmount,-15} {claim.DateOfIncident.ToShortDateString(),-20} {claim.DateOfClaim.ToShortDateString(),-20} {claim.IsValid,-10}");
        }

        // Fills the list at startup
        public void SeedClaims(List<Claims> claims)
        {
            Claims carCrash = new Claims(ClaimType.car, "Truck slid off 465", 2500.00m, new DateTime(2020, 2, 16), true);
            _repo.CreateClaim(carCrash);
            Claims houseExplosion = new Claims(ClaimType.home, "Avengers blew up home", 75000.00m, new DateTime(2016, 3, 22), true);
            _repo.CreateClaim(houseExplosion);
        }
    }
}
