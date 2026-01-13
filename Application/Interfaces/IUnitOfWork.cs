using Domain.Entities;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Customer> Customers { get; }
    IGenericRepository<Order> Orders { get; }
    Task<int> SaveChangesAsync();
}
