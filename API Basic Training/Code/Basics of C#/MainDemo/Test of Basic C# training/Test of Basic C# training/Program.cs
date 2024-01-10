using System;

namespace Test_of_Basic_C__training
{
    internal class Program
    {
        /// <summary>
        /// Main method of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            MenuSystem menu = new MenuSystem();
            DatabaseManagement databaseManagement = new DatabaseManagement();

            Console.WriteLine("Welcome to RKIT banking system\n");
            while (true)
            {
                Console.WriteLine("Are you new here ? 1-YES || 2-NO");
                Console.WriteLine("Press 0  for exit");
                int isUser = int.Parse(Console.ReadLine());

                if (isUser == 2)
                {
                    menu.Login(databaseManagement);
                }
                else if (isUser == 1)
                {
                    Console.Write("Enter your name : ");
                    string name = Console.ReadLine();
                    Console.Write("Enter your Mobile Number : ");
                    string mobileNumber = Console.ReadLine();
                    Console.Write("Enter your birth date (dd-MM-yyyy) : ");
                    string dob = Console.ReadLine();

                    string newPin = ConvertToPin(dob);
                    User newUser = new User(name, newPin, mobileNumber);
                    databaseManagement.AddUser(newUser);
                }
                else if (isUser == 0)
                    break;
                else
                    Console.WriteLine("Invalid Choice");
            }
        }

        #region Helper Methods

        /// <summary>
        /// Converts the user input date to a PIN.
        /// </summary>
        /// <param name="userInput">The user input date in the format dd-MM-yyyy.</param>
        /// <returns>The PIN as a string.</returns>
        public static string ConvertToPin(string userInput)
        {
            // Check if the input has the expected format
            if (userInput.Length == 10 && userInput[2] == '-' && userInput[5] == '-')
            {
                // Extract day and month and concatenate to form a 4-digit PIN
                string dayPart = userInput.Substring(0, 2);
                string monthPart = userInput.Substring(3, 2);

                if (int.TryParse(dayPart, out int day) && int.TryParse(monthPart, out int month))
                {
                    int pin = day * 100 + month;
                    return Convert.ToString(pin);
                }
            }

            Console.WriteLine("Invalid date format. Please enter a date in the format dd-MM-yyyy.");
            return "Error"; // Indicate an error
        }

        #endregion
    }
}

