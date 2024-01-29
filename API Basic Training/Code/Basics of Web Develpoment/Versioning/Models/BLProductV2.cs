namespace Versioning.Models
{
    /// <summary>
    /// Represents a version 2 of a product in the business logic.
    /// </summary>
    public class BLProductV2
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

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
