using Domain.Entities;

namespace Application.Features.Customers.Queries.GetCustomerById;

// We return the Entity directly for simplicity, or we could map to a DTO
public record GetCustomerByIdQuery(int Id) : MediatR.IRequest<Customer?>;
