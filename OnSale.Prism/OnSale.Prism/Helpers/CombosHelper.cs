using System.Collections.Generic;
using OnSale.Common.Models;

namespace OnSale.Prism.Helpers
{
    public class CombosHelper : ICombosHelper
    {

        public IEnumerable<PaymentMethod> GetPaymentMethods()
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod { Id = 1, Name = Languages.Cash },
                new PaymentMethod { Id = 2, Name = Languages.CreditCard }
            };

            return paymentMethods;
        }
    }
}
