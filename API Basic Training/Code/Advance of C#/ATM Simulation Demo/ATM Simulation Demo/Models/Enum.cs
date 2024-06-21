namespace ATM_Simulation_Demo.Models
{
    /// <summary>
    /// Enumeration representing different types of operations.
    /// </summary>
    public enum enmOperation
    {
        /// <summary>
        /// Represents a add operation.
        /// </summary>
        A,

        /// <summary>
        /// Represents a edit operation.
        /// </summary>
        E
    }

    /// <summary>
    /// Represents the roles that a user can have in the system.
    /// </summary>
    public enum enmUserRole
    {
        /// <summary>
        /// User Role
        /// </summary>
        U,

        /// <summary>
        /// Administrator role.
        /// </summary>
        A

    }

    /// <summary>
    /// Enum representing transaction types.
    /// </summary>
    public enum enmTransactionType
    {
        /// <summary>
        /// Debit
        /// </summary>
        D,
        /// <summary>
        /// Creditt
        /// </summary>
        C
    }


    public static class EnumExtensions
    {
        public static string Value(this enmUserRole enumValue)
        {
            switch (enumValue)
            {
                case enmUserRole.U:
                    return "User";
                case enmUserRole.A:
                    return "Administrator";
               default:
                    return string.Empty;
            }
        }
        public static string Value(this enmTransactionType enumTransactionType)
        {
            switch (enumTransactionType)
            {
                case enmTransactionType.C:
                    return "Credit";
                case enmTransactionType.D:
                    return "Debit";
                default:
                    return string.Empty;
            }

        }
    }

}