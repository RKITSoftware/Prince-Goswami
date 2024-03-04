
using System;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// class for managing Transactions
    /// </summary>
    public class TransactionManagement
    {
        #region Private Fields
        private DatabaseManagement _databaseManagement;
        private UserManagement _userManagement;
        #endregion

        #region constructor
        public TransactionManagement(DatabaseManagement _databaseManagement)
        {
            this._databaseManagement = _databaseManagement;
            _userManagement = new UserManagement();
        }
        #endregion

        #region Deposit Cash
        /// <summary>
        /// 
        /// Deposit cash into the user's account
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="amount">The amount to deposit</param>
        public void DepositCash(UserModel user, decimal amount)
        {
            if (amount > 0)
            {
                _userManagement.UpdateBalance(user, amount);
                AddTransaction(user, $"Deposit: {amount}", amount);
                Console.WriteLine($"Deposit successful. New balance: {user.Balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
            }
        }
        #endregion

        #region Withdraw Cash
        /// <summary>
        /// Withdraw cash from the user's account
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="amount">The amount to withdraw</param>
        public void WithdrawCash(UserModel user, decimal amount)
        {
            if (amount > 0 && user.Balance >= amount)
            {
                _userManagement.UpdateBalance(user, -amount);
                AddTransaction(user, $"Withdrawal: {amount:C}", -amount);
                Console.WriteLine($"Withdrawal successful. New balance: {user.Balance:C}");
            }
            else if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
            }
            else
            {
                Console.WriteLine("Insufficient funds. Withdrawal failed.");
            }
        }
        #endregion

        #region View Transaction History
        /// <summary>
        /// View transaction history for a user
        /// </summary>
        /// <param name="user">The user</param>
        public void ViewTransactionHistory(UserModel user)
        {
            Console.WriteLine($"===== Transaction History for {user.CardNumber} =====");
            foreach (Transaction transaction in user.TransactionHistory)
            {
                Console.WriteLine($"{transaction.Date}: {transaction.Description}, Amount: {transaction.Amount}");
            }
        }
        #endregion

        #region Add Transaction
        /// <summary>
        /// Add a transaction to the user's transaction history
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="description">The description of the transaction</param>
        /// <param name="amount">The amount of the transaction</param>
        private void AddTransaction(UserModel user, string description, decimal amount)
        {
            Transaction transaction = new Transaction(description, amount);
            _userManagement.AddTransaction(user, transaction);
            _databaseManagement.UpdateUserData(user);
        }
        #endregion
    }
}