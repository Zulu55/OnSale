using OnSale.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnSale.Web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date")]
        public DateTime DateLocal => Date.ToLocalTime();

        public User User { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date Sent")]
        public DateTime? DateSent { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date Sent")]
        public DateTime? DateSentLocal => DateSent?.ToLocalTime();

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date Confirmed")]
        public DateTime? DateConfirmed { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date Confirmed")]
        public DateTime? DateConfirmedLocal => DateSent?.ToLocalTime();

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public int Lines => OrderDetails == null ? 0 : OrderDetails.Count;

        public float Quantity => OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Quantity);

        public decimal Value => OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Value);
    }
}
