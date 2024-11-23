using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntend(string basketId);
    }
}
