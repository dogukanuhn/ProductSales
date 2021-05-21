﻿using ProductSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Domain.Abstract.Services
{
    public interface IPaymentService
    {
        Task Create(CustomerPayment customerPayment);
    }
}
