using Controller_Initialization.BL;
using System.Security.Principal;

namespace Controller_Initialization.Models
{
    public class BLDateTime : IBLDateTime
    {
        public Guid guid { get => Guid.NewGuid(); }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }

}
