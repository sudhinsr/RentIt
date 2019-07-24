using Microsoft.EntityFrameworkCore;
using RentIt.Models;
using RentIt.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentIt.Repository
{
    public class ProductItemRepository: GenericRepository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(RentItContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ProductItem>> GetProductItemsAsync(int productId)
        {
            return await Query()
                .Where(p => p.ProductId == productId).ToArrayAsync();
        }
    }
}
