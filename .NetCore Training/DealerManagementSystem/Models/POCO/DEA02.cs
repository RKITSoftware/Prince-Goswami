namespace DealerManagementSystem.Models.POCO
{
    public class DEA02 // Dealer Transactions
    {
        ///<summary>
        ///DealerTransID - Unique identifier for each transaction
        ///</summary>
        public int A02F01 { get; set; } // DealerTransID

        ///<summary>
        ///DealerID - Reference to the dealer
        ///</summary>
        public int A02F02 { get; set; } // DealerID

        ///<summary>
        ///VehicleID - Reference to the vehicle involved
        ///</summary>
        public int A02F03 { get; set; } // VehicleID

        ///<summary>
        ///PurchasePrice - Price at which the dealer purchased the vehicle
        ///</summary>
        public decimal A02F04 { get; set; } // PurchasePrice

        ///<summary>
        ///SellingPrice - Price at which the dealer sold the vehicle
        ///</summary>
        public decimal A02F05 { get; set; } // SellingPrice

        ///<summary>
        ///TaxAmount - Applicable taxes on the transaction
        ///</summary>
        public decimal? A02F06 { get; set; } // TaxAmount

        ///<summary>
        ///ProfitMargin - Profit margin on the vehicle sale
        ///</summary>
        public decimal? A02F07 { get; set; } // ProfitMargin

        ///<summary>
        ///TransactionDate - Date and time of the transaction
        ///</summary>
        public DateTime A02F08 { get; set; } // TransactionDate
    }

}
