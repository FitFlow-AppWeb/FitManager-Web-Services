// Members/Domain/Model/Aggregates/MembershipStatus.cs
using System; // Para DateTime

namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    public class MembershipStatus
    {
        public int Id { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public int MemberId { get; set; } 
        public int MembershipTypeId { get; set; }

        public Member Member { get; set; } 
        public MembershipType MembershipType { get; set; }

  
        public MembershipStatus(DateTime startDate, DateTime endDate, string status, int membershipTypeId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            MembershipTypeId = membershipTypeId;
        }

        protected MembershipStatus() { }

        public void UpdateStatus(string newStatus, DateTime newEndDate, int newMembershipTypeId, DateTime? newStartDate = null)
        {
            Status = newStatus;
            EndDate = newEndDate;
            MembershipTypeId = newMembershipTypeId;
            if (newStartDate.HasValue)
            {
                StartDate = newStartDate.Value;
            }
        }
    }
}