using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvoryDesign
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<int> Jobs { get; set; }
        public string Name
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }

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

    public class Payment
    {
        public DateTime Date { get; set; }
        public float Payed { get; set; } 
    }

    public enum JobStatuses
    {
        Quote, //Job is in the quote stage
        Inactive, //Ouote went out but not ordered after 4 Weeks therefore deemed inactive
        Active, //Job has started and being worked on
        WaitFitting, //Waiting for the customer to come for the next fitting
        Complete, //Job is complete and no further work is to be done
        Cancelled, //Customer cancelled the job at some point after the job had started
    }

    public enum PaymentStatuses
    {
        None, //Means that the Job is not active or in Qoute stage
        WaitPayment, //Waiting for next payment from customer
        Active, //Job is active but no payments are expected at the moment
        Paid //Fully paid whether job is complete or not
    }
}
