using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance
{
    public class BadgeRepo
    {
        public readonly Dictionary<int, Badge> _dictionary = new Dictionary<int, Badge>();

        public bool CreateBadge(Badge newBadge)
        {
            int startingCount = _dictionary.Count;
            bool wasCreated;
            _dictionary.Add(newBadge.BadgeID, newBadge);
            wasCreated = _dictionary.Count > startingCount;
            return wasCreated;
        }

        public Dictionary<int, Badge> GetBadges()
        {
            return _dictionary;
        }

        

        public Badge GetBadgeByID(int badgeID)
        {
            return _dictionary[badgeID];
        }

        public bool UpdateBadge(Badge newBadge)
        {
            Badge oldBadge = GetBadgeByID(newBadge.BadgeID);
            if (oldBadge != null)
            {
                oldBadge.BadgeID = newBadge.BadgeID;
                oldBadge.DoorAccess = newBadge.DoorAccess;
                return true;
            }
            return false;
        }

        public bool DeleteBadge(Badge removeBadge)
        {
            if (removeBadge != null)
            {
                _dictionary.Remove(removeBadge.BadgeID);
                return true;
            }
            return false;
        }

        public void Seed()
        {
            List<string> seedBadge = new List<string>() { "A4", "B2" };
            List<string> seedBadge2 = new List<string>() { "C1", "A4" };
            Badge badge1 = new Badge(seedBadge, 12345);
            Badge badge2 = new Badge(seedBadge2, 22345);
            _dictionary.Add(badge1.BadgeID, badge1);
            _dictionary.Add(badge2.BadgeID, badge2);
        }
    }
}
