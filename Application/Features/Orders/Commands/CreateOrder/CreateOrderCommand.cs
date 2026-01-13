using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

// Request returns an "int" (the ID of the created order)
public record CreateOrderCommand(int CustomerId, decimal TotalAmount) : IRequest<int>;
