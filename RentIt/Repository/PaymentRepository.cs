﻿using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
    }
}
