using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvoryDesign.Data
{
    public enum JobStatuses
    {
        Quote, //Job is in the quote stage
        Inactive, //Quote went out but not ordered after 4 Weeks therefore deemed inactive
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
