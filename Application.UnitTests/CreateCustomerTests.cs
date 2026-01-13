using Application.Features.Customers.Commands.CreateCustomer;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Features.Customers.Commands;

public class CreateCustomerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCustomerHandler _handler;

    public CreateCustomerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(u => u.Customers.AddAsync(It.IsAny<Customer>()))
            .Returns(Task.CompletedTask);
        _handler = new CreateCustomerHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_Should_CallRepository_And_ReturnId()
    {
        var command = new CreateCustomerCommand("Test User", "test@test.com", "123 Street", "555-5555");
        var result = await _handler.Handle(command, CancellationToken.None);
        
        _mockUnitOfWork.Verify(x => x.Customers.AddAsync(It.IsAny<Customer>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        result.Should().Be(0); 
    }
}
