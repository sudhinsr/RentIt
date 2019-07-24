using RentIt.Models;
using RentIt.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentIt.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
    }
}
