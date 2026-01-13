using MediatR;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Business Logic: Check if customer exists
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            // In a real app, you might use a custom NotFoundException here
            throw new Exception($"Customer with ID {request.CustomerId} not found.");
        }

        // 2. Map Command to Entity
        var order = new Order
        {
            CustomerId = request.CustomerId,
            TotalAmount = request.TotalAmount,
            OrderDate = DateTime.UtcNow
        };

        // 3. Save using Unit of Work
        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order.Id;
    }
}
