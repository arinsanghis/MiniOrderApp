using MediatR;

namespace Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string Name, string Email, string Address, string PhoneNumber) : IRequest<int>;
