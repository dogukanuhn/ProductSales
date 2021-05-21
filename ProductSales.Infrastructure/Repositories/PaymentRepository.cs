using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<CustomerPayment>, IPaymentRepository
    {
        public PaymentRepository(IMongoDbContext<CustomerPayment> context) : base(context)
        {
        }
    }
}
