using DDD.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string? UserId { get; }
}

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    
}