using MediatR;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(IUnitOfWork unitOfWork, ILogger<CreateOrderHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            _logger.LogWarning("Attempted to create order for non-existent Customer ID: {CustomerId}", request.CustomerId);
            throw new Exception($"Customer with ID {request.CustomerId} not found.");
        }

        var order = new Order
        {
            CustomerId = request.CustomerId,
            TotalAmount = request.TotalAmount,
            OrderDate = DateTime.UtcNow
        };

        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} created successfully for Customer {CustomerId}", order.Id, request.CustomerId);
        return order.Id;
    }
}
