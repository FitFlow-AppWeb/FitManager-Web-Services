using System;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    public class Booking
    {
        public int Id { get; private set; }

        public int MemberId { get; set; }
        public int ClassId { get; set; }
        public DateTime Date { get; set; }

        public Member Member { get; set; }
        public Class Class { get; set; }

        public Booking(int memberId, int classId, DateTime date)
        {
            MemberId = memberId;
            ClassId = classId;
            Date = date;
        }

        protected Booking() { }
    }
}
