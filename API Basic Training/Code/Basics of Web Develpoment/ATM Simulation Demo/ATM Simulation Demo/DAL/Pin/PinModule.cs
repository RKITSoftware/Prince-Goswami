using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using System;

namespace ATM_Simulation_Demo.DAL.Pin
{
    /// <summary>
    /// Implementation of the PIN module interface.
    /// </summary>
    public class PinModule : IBLPinModule
    {
        #region Public Methods

        /// <summary>
        /// Assigns a new PIN to a user.
        /// </summary>
        /// <param name="user">The user to assign the PIN to.</param>
        /// <param name="newPin">The new PIN to assign.</param>
        public void AssignPin(BLAccountModel user, string newPin)
        {
            if (IsPinValid(newPin))
            {
                user.PIN = newPin;
                Console.WriteLine("PIN assigned successfully.");
            }
            else
            {
                Console.WriteLine("Invalid PIN. Please use a 4-digit numeric PIN.");
            }
        }

        /// <summary>
        /// Changes the PIN for a user.
        /// </summary>
        /// <param name="user">The user to change the PIN for.</param>
        /// <param name="currentPin">The current PIN for verification.</param>
        /// <param name="newPin">The new PIN to set.</param>
        public void ChangePin(BLAccountModel user, string currentPin, string newPin)
        {
            if (VerifyPin(user, currentPin))
            {
                if (IsPinValid(newPin))
                {
                    user.PIN = newPin;
                    Console.WriteLine("PIN changed successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid new PIN. Please use a 4-digit numeric PIN.");
                }
            }
            else
            {
                Console.WriteLine("Current PIN verification failed. PIN not changed.");
            }
        }

        /// <summary>
        /// Verifies if the entered PIN matches the user's PIN.
        /// </summary>
        /// <param name="user">The user to verify the PIN for.</param>
        /// <param name="enteredPin">The entered PIN to verify.</param>
        /// <returns>True if the entered PIN matches the user's PIN, otherwise false.</returns>
        public bool VerifyPin(BLAccountModel user, string enteredPin)
        {
            return user.PIN == enteredPin;
        }

        #endregion

        #region Helper Methods

        // Helper method to validate the PIN format
        private bool IsPinValid(string pin)
        {
            // PIN should be a 4-digit numeric value
            return !string.IsNullOrEmpty(pin) && pin.Length == 4 && int.TryParse(pin, out _);
        }

        #endregion
    }
}
