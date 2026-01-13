using MediatR;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetOrdersByCustomerId;

public record GetOrdersByCustomerIdQuery(int CustomerId) : IRequest<List<Order>>;

public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, List<Order>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByCustomerIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Orders.Query()
            .Where(o => o.CustomerId == request.CustomerId)
            .ToListAsync(cancellationToken);
    }
}
