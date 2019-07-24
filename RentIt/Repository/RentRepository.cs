using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class RentRepository : GenericRepository<Rent>, IRentRepository
    {
        public RentRepository(RentItContext context)
        {
            _dbContext = context;
        }
    }
}
