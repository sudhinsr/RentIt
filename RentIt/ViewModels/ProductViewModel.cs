using RentIt.Models;

namespace RentIt.ViewModels
{
    public class ProductViewModel : Product
    {
        public int TotalProductCount { get; set; }
        public int AvilableProductCount { get; set; }
    }
}
