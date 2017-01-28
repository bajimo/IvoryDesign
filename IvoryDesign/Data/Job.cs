using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvoryDesign.Data
{
    public class Job
    {
        public int JobNumber { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public int HoursQuoted { get; set; }
        public int HoursLeft { get { return HoursQuoted - HoursWorked; } }
        public int HoursWorked { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime NextFittingDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public JobStatuses JobStatus { get; set; }
        public PaymentStatuses PaymentStatus { get; set; }
        public float NextPaymentValue { get; set; }
        public float TotalCost { get; set; }
        public float Balance
        {
            get
            {
                float total = 0;
                foreach (Payment payment in Payments)
                {
                    total += payment.Payed;
                }

                return TotalCost - total;
            }
        }
        public List<Payment> Payments { get; set; }
    }
}
