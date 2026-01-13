using MediatR;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        // This satisfies the requirement: "GetCustomerById including profile and orders"
        return await _unitOfWork.Customers.Query()
            .Include(c => c.Profile)
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
    }
}
