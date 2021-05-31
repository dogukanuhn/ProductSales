using ProductSales.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Application.Services
{
    public interface IAuthenticationService
    {
        Task<IDataResult<Dictionary<string, string>>> LoginSeller(string email, string password);
        Task<IDataResult<Dictionary<string, string>>> LoginCustomer(string email, string password);
    }
}
