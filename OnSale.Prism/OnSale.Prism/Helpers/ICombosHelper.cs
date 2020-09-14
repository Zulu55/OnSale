using System.Collections.Generic;
using OnSale.Common.Models;

namespace OnSale.Prism.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<PaymentMethod> GetPaymentMethods();
    }
}
