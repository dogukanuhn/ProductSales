using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Domain.ValueObjects
{
    public class State : ValueObject
    {

        public PaymentStatus Status { get; set; }



    }

    public enum PaymentStatus
    {
        Completed,
        Pending,

        Canceled
    }
}
