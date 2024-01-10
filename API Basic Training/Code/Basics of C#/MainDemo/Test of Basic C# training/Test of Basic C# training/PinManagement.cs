using System;

namespace Test_of_Basic_C__training
{
    public class PinModule
    {
        // Method to assign a PIN to a user
        public void AssignPin(User user, string newPin)
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

        // Method to change the PIN for a user
        public void ChangePin(User user, string currentPin, string newPin)
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

        // Method to verify if the entered PIN matches the user's PIN
        public bool VerifyPin(User user, string enteredPin)
        {
            return user.PIN == enteredPin;
        }

        // Helper method to validate the PIN format
        private bool IsPinValid(string pin)
        {
            // PIN should be a 4-digit numeric value
            return !string.IsNullOrEmpty(pin) && pin.Length == 4 && int.TryParse(pin, out _);
        }
    }

}
