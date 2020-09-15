using OnSale.Common.Entities;
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

        public User User { get; set; }

        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Date Sent")]
        public DateTime? DateSent { get; set; }

        [Display(Name = "Date Confirmed")]
        public DateTime? DateConfirmed { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => OrderDetails == null ? 0 : OrderDetails.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Quantity => OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => OrderDetails == null ? 0 : OrderDetails.Sum(od => od.Value);
    }
}
