using FinalDemo.Models;
using FinalDemo.Models.POCO;

namespace FinalDemo.Filters
{
    public class EnumConverter
    {
        public static string GetRoleFullName(enmUserRole roleChar)
        {
            //switch (roleChar)
            //{
            //    case UserRole.M:
            //        return "Master";
            //    case UserRole.A:
            //        return "Admin";
            //    case UserRole.E:
            //        return "Employee";
            //    case UserRole.D:
            //        return "Dealer";
            //    case UserRole.C:
            //        return "Customer";
            //    default:
            //        throw new ArgumentException("Invalid role character.");
            //}
            return "Admin";
        }
    }
}
