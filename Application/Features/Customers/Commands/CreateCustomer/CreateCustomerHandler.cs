using MediatR;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Create the Customer entity
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            // Create the linked Profile entity (1-to-1 relationship)
            Profile = new CustomerProfile
            {
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            }
        };

        // Adding the Customer automatically adds the Profile because of the relationship
        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        return customer.Id;
    }
}
