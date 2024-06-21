namespace FinalDemo.Models.POCO
{

    public class CUS02 // Customer Transactions
    {
        ///<summary>Unique identifier for each transaction</summary>
        public int S02F01 { get; set; } // CustTransID

        ///<summary>Reference to the customer</summary>
        public int S02F02 { get; set; } // CustomerID

        ///<summary>Reference to the vehicle involved</summary>
        public int S02F03 { get; set; } // VehicleID

        ///<summary>Price at which the customer bought the vehicle</summary>
        public decimal S02F04 { get; set; } // SalePrice

        ///<summary>Applicable taxes on the transaction</summary>
        public decimal? S02F05 { get; set; } // TaxAmount

        ///<summary>Date and time of the transaction</summary>
        public DateTime S02F06 { get; set; } // TransactionDate
    }
}
