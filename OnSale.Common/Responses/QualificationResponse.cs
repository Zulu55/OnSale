using System;

namespace OnSale.Common.Responses
{
    public class QualificationResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public float Score { get; set; }

        public string Remarks { get; set; }
    }
}
