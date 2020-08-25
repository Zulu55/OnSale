using System.ComponentModel.DataAnnotations;

namespace OnSale.Common.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public Products Product { get; set; }

        public float Quantity { get; set; }

        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public decimal Value => (decimal)Quantity * Price;
    }
}
