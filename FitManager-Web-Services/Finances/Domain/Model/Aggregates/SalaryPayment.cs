using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    public class SalaryPayment
    {
        public int Id { get; private set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public string Method { get; set; }

        public string CurrencyType { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public SalaryPayment(DateTime date, float amount, string method, string currencyType, int employeeId)
        {
            Date = date;
            Amount = amount;
            Method = method;
            CurrencyType = currencyType;
            EmployeeId = employeeId;
        }

        protected SalaryPayment() { }
    }
}