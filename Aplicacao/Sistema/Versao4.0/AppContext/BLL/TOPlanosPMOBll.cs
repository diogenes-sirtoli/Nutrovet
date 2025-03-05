using System;

namespace BLL
{
    public class TOPlanoPMOBll
    {
        public int Amount { get; set; }
        public int? Charges { get; set; }
        public string Color { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Days { get; set; }
        public string Id { get; set; }
        public int? Installments { get; set; }
        public int? InvoiceReminder { get; set; }
        public string CardNumber { get; set; }
        public bool Loaded { get; set; }
        public string Name { get; set; }
        public int TrialDays { get; set; }
        public string ErrorMessage { get; set; }
        public bool ErrorHash { get; set; }
    }
}
