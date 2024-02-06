using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Simulation_Demo.BAL.Interface.V1
{
    /// <summary>
    /// Interface for managing PINs for users.
    /// </summary>
    public interface IBLPinModule
    {
        /// <summary>
        /// Assigns a PIN to a user.
        /// </summary>
        /// <param name="user">The user to assign the PIN to.</param>
        /// <param name="newPin">The new PIN to assign.</param>
        void AssignPin(BLAccountModel user, string newPin);

        /// <summary>
        /// Changes the PIN for a user.
        /// </summary>
        /// <param name="user">The user to change the PIN for.</param>
        /// <param name="currentPin">The current PIN for verification.</param>
        /// <param name="newPin">The new PIN to set.</param>
        void ChangePin(BLAccountModel user, string currentPin, string newPin);

        /// <summary>
        /// Verifies if the entered PIN matches the user's PIN.
        /// </summary>
        /// <param name="user">The user to verify the PIN for.</param>
        /// <param name="enteredPin">The entered PIN to verify.</param>
        /// <returns>True if the entered PIN matches the user's PIN, otherwise false.</returns>
        bool VerifyPin(BLAccountModel user, string enteredPin);
    }

}
