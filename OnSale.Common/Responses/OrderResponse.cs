using OnSale.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnSale.Common.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public UserResponse User { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime? DateSent { get; set; }

        public DateTime? DateSentLocal => DateSent?.ToLocalTime();

        public DateTime? DateConfirmed { get; set; }

        public DateTime? DateConfirmedLocal => DateSent?.ToLocalTime();

        public string Remarks { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderDetailResponse> OrderDetails { get; set; }

        public int Lines => OrderDetails == null ? 0 : OrderDetails.Count;

        public float Quantity => OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Quantity);

        public decimal Value => OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Value).Value;
    }
}
