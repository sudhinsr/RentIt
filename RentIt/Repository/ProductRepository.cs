using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentIt.Models;
using RentIt.Models.Enum;
using RentIt.Repository.Interface;
using RentIt.ViewModels;

namespace RentIt.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(RentItContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            return await Query().Include(_ => _.ProductItem).Select(_ => new ProductViewModel
            {
                ProductId = _.ProductId,
                Description = _.Description,
                Code = _.Code,
                Name = _.Name,
                Amount = _.Amount,
                AvilableProductCount = _.ProductItem.Count(c => c.Status == ProductItemStatus.Available),
                TotalProductCount = _.ProductItem.Count(),
            }).ToArrayAsync();
        }
    }
}
