using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance
{
    public class Badge
    {
        public int BadgeID { get; set; }
        public List<string> DoorAccess { get; set; }

        public Badge() { }

        public Badge(List<string> doorAccess, int badgeID)
        {
            DoorAccess = doorAccess;
            BadgeID = badgeID;
        }
    }
}
