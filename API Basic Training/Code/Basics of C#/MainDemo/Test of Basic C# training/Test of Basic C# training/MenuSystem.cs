using System;

namespace Test_of_Basic_C__training
{

    public class MenuSystem
    {
        private User loggedInUser;
        private PinModule pinModule;
        private TransactionManagement transactionManagement;
        private DatabaseManagement databaseManagement;
        public MenuSystem()
        {
            pinModule = new PinModule();
            transactionManagement = new TransactionManagement(databaseManagement);
        }

        // Method to handle user login
        public void Login(DatabaseManagement database)
        {
            Console.WriteLine("===== User Login =====");

            Console.Write("Enter your Card Number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter your PIN: ");
            string enteredPin = Console.ReadLine();

            User user = database.GetUser(cardNumber, enteredPin);

            if (user != null)
            {
                Console.WriteLine("Login successful. Welcome!");
                loggedInUser = user;
                ShowMainMenu();
            }
            else
            {
                Console.WriteLine("Login failed. Invalid card number or PIN.");
            }
        }

        // Display the main menu and handle user selections
        private void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("===== Main Menu =====");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. View Transaction History");
                Console.WriteLine("3. Deposit Cash");
                Console.WriteLine("4. Withdraw Cash");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. Update Mobile Number");
                Console.WriteLine("7. Logout");

                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Your Balance: {loggedInUser.Balance:C}");
                        break;

                    case "2":
                        transactionManagement.ViewTransactionHistory(loggedInUser);
                        break;

                    case "3":
                        // Implement cash withdrawal functionality
                        Console.WriteLine("Enter amount of cash to be withdrawn : ");
                        decimal amountToDeposit = decimal.Parse(Console.ReadLine());
                        transactionManagement.DepositCash(loggedInUser , amountToDeposit );
                        break;
                        
                    case "4":
                        // Implement cash withdrawal functionality
                        Console.WriteLine("Enter amount of cash to be withdrawn : ");
                        decimal amountToWithdraw = decimal.Parse(Console.ReadLine());
                        transactionManagement.WithdrawCash(loggedInUser , amountToWithdraw );
                        break;

                    case "5":
                        // Implement change PIN functionality
                        Console.WriteLine("Enter Current pin : ");
                        string currentPin = Console.ReadLine();
                        string newPin = Console.ReadLine();
                        pinModule.ChangePin(loggedInUser, currentPin, newPin);
                        break;

                    case "6":
                        // Implement update mobile number functionality
                        string newMobileNumber = Console.ReadLine();
                        databaseManagement.UpdateMobileNumber(loggedInUser , newMobileNumber);
                        break;

                    case "7":
                        Console.WriteLine("Logging out. Have a nice day!");
                        loggedInUser = null;
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }
    }

}