using System;

namespace RentIt.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public int RentId { get; set; }
        public int PhoneNo { get; set; }
        public DateTime CreatedDate { get; set; }

        public Rent Rent { get; set; }
    }
}
