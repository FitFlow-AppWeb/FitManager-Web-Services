using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    public class Attendance
    {
        public int Id { get; private set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public Attendance(DateTime entryTime, DateTime exitTime, int memberId, int classId)
        {
            EntryTime = entryTime;
            ExitTime = exitTime;
            MemberId = memberId;
            ClassId = classId;
        }

        protected Attendance() { }
    }
}