namespace DealerManagementSystem.Models.POCO
{
    public class DEA01 // Dealer Management
    {
        ///<summary>
        ///DealerID - Unique identifier for each dealer
        ///</summary>
        public int A01F01 { get; set; } // DealerID

        ///<summary>
        ///DealerName - Name of the dealer
        ///</summary>
        public string A01F02 { get; set; } // DealerName

        ///<summary>
        ///ContactInfo - Contact information for the dealer
        ///</summary>
        public string A01F03 { get; set; } // ContactInfo

        ///<summary>
        ///Address - Physical address of the dealer
        ///</summary>
        public string A01F04 { get; set; } // Address
    }

}
