using System.Collections.Generic;

namespace RentIt.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }

        public ICollection<ProductItem> ProductItem { get; set; }
    }
}
