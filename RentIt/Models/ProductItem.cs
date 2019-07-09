using RentIt.Models.Enum;
using System.Collections.Generic;

namespace RentIt.Models
{
    public class ProductItem
    {
        public int ProductItemId { get; set; }
        public int ProductId { get; set; }
        public string Remarks { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public ProductItemStatus Status { get; set; }

        public Product Product { get; set; }
        public ICollection<Rent> Rent { get; set; }

    }
}
