using RentIt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentIt.Repository.Interface
{
    public interface IProductItemRepository : IGenericRepository<ProductItem>
    {
        Task<IEnumerable<ProductItem>> GetProductItemsAsync(int productId);
    }
}
