using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims
{
    public class ClaimsRepo
    {
        private List<Claims> _repo;

        public ClaimsRepo(List<Claims> claimList)
        {
            _repo = claimList;
        }

        public bool CreateClaim(Claims newClaim)
        {
            int startingCount = _repo.Count;
            _repo.Add(newClaim);
            newClaim.ClaimID = _repo.Count;
            bool wasAdded = startingCount < _repo.Count;
            return wasAdded;
        }

        public List<Claims> GetClaims()
        {
            return _repo;
        }

        public Claims GetClaimByID(int claimID)
        {
            foreach (Claims claim in _repo)
            {
                if (claimID == claim.ClaimID)
                {
                    return claim;
                }
            }
            return null;
        }

        public bool DeleteClaim(Claims deletedClaim)
        {
            int startingCount = _repo.Count;
            _repo.Remove(deletedClaim);
            
            //Making sure not to leave gaps in the claimIDs
            int IDFix = 1;
            foreach (Claims claim in _repo)
            {
                claim.ClaimID = IDFix;
                IDFix++;
            }
            bool wasRemoved = startingCount > _repo.Count;
            return wasRemoved;
        }
    }
}
