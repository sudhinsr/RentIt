using RentIt.Models.Enum;
using System;
using System.Collections.Generic;

namespace RentIt.Models
{
    public class Rent
    {
        public int RentId { get; set; }
        public int ProductItemId { get; set; }
        public decimal Amount { get; set; }
        public int PhoneNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public RentStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public ProductItem ProductItem { get; set; }
        public ICollection<Payment> Payment { get; set; }

    }
}
