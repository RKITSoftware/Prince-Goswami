namespace DealerManagementSystem.Models.POCO
{
    public enum UserRole
    {
        M, //Master
        A, //Admin
        E, //Employee
        D, //Dealer
        C //Customer
    }

    public class USR01 // User Management
    {
        ///<summary>
        ///UserID - Unique identifier for each user
        ///</summary>
        public int R01F01 { get; set; } // UserID

        ///<summary>
        ///Username - User's login name
        ///</summary>
        public string R01F02 { get; set; } // Username

        ///<summary>
        ///PasswordHash - Hash of the user's password
        ///</summary>
        public string R01F03 { get; set; } // PasswordHash

        ///<summary>
        ///Role - Role of the user
        ///</summary>
        public UserRole R01F04 { get; set; } // Role

        ///<summary>
        ///Email - User's email address
        ///</summary>
        public string R01F05 { get; set; } // Email

        ///<summary>
        ///CreatedAt - Account creation date and time
        ///</summary>
        public DateTime R01F06 { get; set; } // CreatedAt
    }
}
