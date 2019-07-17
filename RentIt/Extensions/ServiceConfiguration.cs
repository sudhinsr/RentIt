using Microsoft.Extensions.DependencyInjection;
using RentIt.Repository;
using RentIt.Repository.Interface;

namespace RentIt.Extensions
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection service)
        {
            service.AddScoped<IAddressRepository, AddressRepository>();
            service.AddScoped<IProductItemRepository, ProductItemRepository>();
            service.AddScoped<IPaymentRepository, PaymentRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IRentRepository, RentRepository>();

            return service;
        }
    }
}
