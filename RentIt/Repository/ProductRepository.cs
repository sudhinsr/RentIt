using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(RentItContext context)
        {
            _dbContext = context;
        }
    }
}
