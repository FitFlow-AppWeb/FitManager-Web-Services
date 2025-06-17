using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    public class MembershipPayment
    {
        public int Id { get; private set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public string Method { get; set; }

        public string Currency { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }

        public MembershipPayment(DateTime date, float amount, string method, string currency, int memberId)
        {
            Date = date;
            Amount = amount;
            Method = method;
            Currency = currency;
            MemberId = memberId;
        }

        protected MembershipPayment() { }
    }
}