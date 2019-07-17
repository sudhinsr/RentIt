using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class ProductItemRepository: GenericRepository<ProductItem>, IProductItemRepository
    {
    }
}
