using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class ProductItemRepository: GenericRepository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(RentItContext context)
        {
            _dbContext = context;
        }
    }
}
