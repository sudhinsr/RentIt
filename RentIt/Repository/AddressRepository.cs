using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(RentItContext context)
        {
            _dbContext = context;
        }
    }
}
