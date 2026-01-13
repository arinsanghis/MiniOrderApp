using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Queries.GetCustomerById;
using Application.Features.Orders.Commands.CreateOrder;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;

namespace Application.UnitTests;

public class EndToEndTests
{
    private readonly AppDbContext _dbContext;
    private readonly UnitOfWork _unitOfWork;
    private readonly Mock<ILogger<CreateOrderHandler>> _mockLogger;

    public EndToEndTests()
    {
        // 1. Setup In-Memory Database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureCreated();

        // 2. Setup Unit of Work
        _unitOfWork = new UnitOfWork(_dbContext);
        _mockLogger = new Mock<ILogger<CreateOrderHandler>>();
    }

    [Fact]
    public async Task CompleteUserFlow_Should_CreateCustomer_And_PlaceOrder()
    {
        // --- STEP 1: CREATE CUSTOMER ---
        var createCustomerHandler = new CreateCustomerHandler(_unitOfWork);
        
        // FIX: Use Constructor arguments instead of property initializers
        var customerCommand = new CreateCustomerCommand("Arin Sanghi", "test@example.com", "Chennai, India", "1234567890");

        var customerId = await createCustomerHandler.Handle(customerCommand, CancellationToken.None);
        
        customerId.Should().BeGreaterThan(0);

        // --- STEP 2: CREATE ORDER ---
        var createOrderHandler = new CreateOrderHandler(_unitOfWork, _mockLogger.Object);
        
        // FIX: Use Constructor arguments
        var orderCommand = new CreateOrderCommand(customerId, 999.99m);

        var orderId = await createOrderHandler.Handle(orderCommand, CancellationToken.None);

        orderId.Should().BeGreaterThan(0);

        // --- STEP 3: QUERY EVERYTHING ---
        var queryHandler = new GetCustomerByIdHandler(_unitOfWork);
        
        // FIX: Use Constructor arguments
        var query = new GetCustomerByIdQuery(customerId);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        // --- FINAL VERIFICATION ---
        result.Should().NotBeNull();
        result.Name.Should().Be("Arin Sanghi");
        result.Profile.Should().NotBeNull();
        result.Profile!.Address.Should().Be("Chennai, India");
        result.Orders.Should().HaveCount(1);
        result.Orders.First().TotalAmount.Should().Be(999.99m);
    }
}
