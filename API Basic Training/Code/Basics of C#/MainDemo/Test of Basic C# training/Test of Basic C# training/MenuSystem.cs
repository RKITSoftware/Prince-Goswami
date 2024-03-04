using System;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// Class representing the menu system of the application.
    /// </summary>
    public class MenuSystem
    {
        #region Fields

        private UserModel _loggedInUser;
        private TransactionManagement _transactionManagement;
        private DatabaseManagement _databaseManagement;
        private UserManagement _userManagement;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for MenuSystem class.
        /// </summary>
        public MenuSystem()
        {
            _userManagement = new UserManagement();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to handle user login.
        /// </summary>
        /// <param name="database">The database management object.</param>
        public void Login(DatabaseManagement database)
        {
            Console.WriteLine("===== User Login =====");

            Console.Write("Enter your Card Number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter your PIN: ");
            string enteredPin = Console.ReadLine();

            UserModel user = database.GetUser(cardNumber, enteredPin);

            if (user != null)
            {
                Console.WriteLine("Login successful. Welcome "+ user.UserName);
                _loggedInUser = user;
                _databaseManagement = database;
                _transactionManagement = new TransactionManagement(_databaseManagement);
                ShowMainMenu();
            }
            else
            {
                Console.WriteLine("Login failed. Invalid card number or PIN.");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Display the main menu and handle user selections.
        /// </summary>
        private void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("===== Main Menu =====");
                Console.WriteLine("0. Log Out");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. View Transaction History");
                Console.WriteLine("3. Deposit Cash");
                Console.WriteLine("4. Withdraw Cash");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. Update Mobile Number");
                Console.WriteLine("7. Display User Information");

                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        // Logout
                        Console.WriteLine("Logging out. Have a nice day!");
                        _loggedInUser = null;
                        return;

                    case "1":
                        // Display user's balance
                        Console.WriteLine($"Your Balance: {_loggedInUser.Balance:C}");
                        break;

                    case "2":
                        // View transaction history
                        _transactionManagement.ViewTransactionHistory(_loggedInUser);
                        break;

                    case "3":
                        // Deposit cash
                        Console.WriteLine("Enter amount of cash to be deposited: ");
                        decimal amountToDeposit = decimal.Parse(Console.ReadLine());
                        _transactionManagement.DepositCash(_loggedInUser, amountToDeposit);
                        break;

                    case "4":
                        // Withdraw cash
                        Console.WriteLine("Enter amount of cash to be withdrawn: ");
                        decimal amountToWithdraw = decimal.Parse(Console.ReadLine());
                        _transactionManagement.WithdrawCash(_loggedInUser, amountToWithdraw);
                        break;

                    case "5":
                        // Change PIN
                        Console.WriteLine("Enter Current PIN: ");
                        string currentPin = Console.ReadLine();
                        Console.WriteLine("Enter New PIN: ");
                        string newPin = Console.ReadLine();
                        _databaseManagement.ChangePin(_loggedInUser, currentPin, newPin);
                        break;

                    case "6":
                        // Update mobile number
                        Console.WriteLine("Enter New Mobile Number: ");
                        string newMobileNumber = Console.ReadLine();
                        _databaseManagement.UpdateMobileNumber(_loggedInUser, newMobileNumber);
                        break;

                    case "7":
                        //Display User
                        _userManagement.DisplayUserDetails(_loggedInUser);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }

        #endregion
    }
}