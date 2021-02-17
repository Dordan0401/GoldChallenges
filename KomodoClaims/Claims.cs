using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims
{
    public enum ClaimType { car, home, theft }
    public class Claims
    {
        public int ClaimID { get; set; }
        public ClaimType ClaimType { get; set; }
        public string Description { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime DateOfClaim { get; set; }
        public bool IsValid { get; set; }

        public Claims() { }

        public Claims(ClaimType claimType, string description, decimal claimAmt, DateTime incedentDate, bool isValid)
        {
            ClaimType = claimType;
            Description = description;
            ClaimAmount = claimAmt;
            DateOfIncident = incedentDate;
            IsValid = isValid;
            DateOfClaim = DateTime.Now;
        }
    }
}
