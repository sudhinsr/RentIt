using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    }
}
