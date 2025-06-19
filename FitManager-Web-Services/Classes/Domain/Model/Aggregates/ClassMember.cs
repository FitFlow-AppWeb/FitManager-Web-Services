using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    public class ClassMember
    {
        public int MemberId { get; set; }
        public int ClassId { get; set; }
        
        public Member Member { get; set; }
        public Class Class { get; set; }

        public ClassMember(int memberId, int classId)
        {
            MemberId = memberId;
            ClassId = classId;
        }

        protected ClassMember() { }
    }
}