using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Inventory.Domain.Model.Aggregates
{
    public class ItemBooking
    {
        public int Id { get; private set; }

        public DateTime ReservationDate { get; set; }
        public int UsageTime { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public ItemBooking(DateTime reservationDate, int usageTime, int classId, int itemId)
        {
            ReservationDate = reservationDate;
            UsageTime = usageTime;
            ClassId = classId;
            ItemId = itemId;
        }

        protected ItemBooking() { }
    }
}