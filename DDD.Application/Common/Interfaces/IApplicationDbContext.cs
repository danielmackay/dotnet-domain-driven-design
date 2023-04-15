using DDD.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
}