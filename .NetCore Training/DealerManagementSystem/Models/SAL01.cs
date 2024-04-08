namespace DealerManagementSystem.Models
{
    public class SAL01 // Sales and Deal Structuring
    {
        ///<summary>
        ///DealID - Unique identifier for each deal
        ///</summary>
        public int L01F01 { get; set; } // DealID

        ///<summary>
        ///CustomerID - Reference to the customer
        ///</summary>
        public int L01F02 { get; set; } // CustomerID

        ///<summary>
        ///VehicleID - Reference to the vehicle being sold
        ///</summary>
        public int L01F03 { get; set; } // VehicleID

        ///<summary>
        ///SalePrice - Final sale price of the vehicle
        ///</summary>
        public decimal L01F04 { get; set; } // SalePrice

        ///<summary>
        ///Date - Date of the deal
        ///</summary>
        public DateTime L01F05 { get; set; } // Date

        ///<summary>
        ///CreditApproved - Whether credit was approved
        ///</summary>
        public bool L01F06 { get; set; } // CreditApproved

        ///<summary>
        ///ContractSigned - Whether the contract was signed
        ///</summary>
        public bool L01F07 { get; set; } // ContractSigned
    }
}
