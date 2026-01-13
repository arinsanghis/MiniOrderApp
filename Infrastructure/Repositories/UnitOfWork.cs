using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public IGenericRepository<Customer> Customers { get; }
    public IGenericRepository<Order> Orders { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Customers = new GenericRepository<Customer>(_context);
        Orders = new GenericRepository<Order>(_context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
