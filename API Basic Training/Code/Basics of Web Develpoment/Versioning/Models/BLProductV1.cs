namespace Versioning.Models
{
    /// <summary>
    /// Represents a version 1 of a product in the business logic.
    /// </summary>
    public class BLProductV1
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
