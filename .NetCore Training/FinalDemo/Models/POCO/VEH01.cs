namespace FinalDemo.Models.POCO
{
    public class VEH01 // Vehicle Inventory
    {
        ///<summary>
        ///PartID - Unique identifier for each vehicle
        ///</summary>
        public int H01F01 { get; set; } // PartID

        ///<summary>
        ///Name - Name of the vehicle
        ///</summary>
        public string H01F02 { get; set; } // Name

        ///<summary>
        ///Description - Description of the vehicle
        ///</summary>
        public string H01F03 { get; set; } // Description

        ///<summary>
        ///Price - Price of the vehicle
        ///</summary>
        public decimal H01F04 { get; set; } // Price

        ///<summary>
        ///StockQuantity - Quantity of the vehicle in stock
        ///</summary>
        public int H01F05 { get; set; } // StockQuantity

        ///<summary>
        ///ReorderLevel - Level at which to reorder the vehicle
        ///</summary>
        public int H01F06 { get; set; } // ReorderLevel

        ///<summary>
        ///CategoryID - Category ID of the vehicle
        ///</summary>
        public int H01F07 { get; set; } // CategoryID
    }

}
