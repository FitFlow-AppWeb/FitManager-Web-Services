namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    public class MembershipType
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Duration { get; set; }
        public string Benefits { get; set; }

        public ICollection<MembershipStatus> MembershipStatuses { get; set; } = new List<MembershipStatus>();

        public MembershipType(string name, string description, int price, string duration, string benefits)
        {
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
            Benefits = benefits;
        }

        protected MembershipType() { }
    }
}