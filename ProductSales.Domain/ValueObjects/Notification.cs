using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Domain.ValueObjects
{
    public class Notification : ValueObject
    {
        public bool Email { get; set; }
        public bool Sms { get; set; }

    }
}
