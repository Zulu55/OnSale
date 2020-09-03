using System.ComponentModel.DataAnnotations;

namespace OnSale.Common.Requests
{
    public class QualificationRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Range(0, 5)]
        [Required]
        public float Score { get; set; }

        public string Remarks { get; set; }
    }
}
