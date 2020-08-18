using System.ComponentModel.DataAnnotations;

namespace OnSale.Common.Requests
{
    public class EmailRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
