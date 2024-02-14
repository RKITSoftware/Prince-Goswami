
namespace Advance_C__Final_Demo.Models
{

    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public int  R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the card number associated with the user.
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the personal identification number (PIN) for the user.
        /// </summary>
        public int R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the user.
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Gets or sets the current balance in the user's account.
        /// </summary>
        public decimal R01F05 { get; set; }
    }


}