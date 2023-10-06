using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Domain.ValueObjects;

namespace StoneChallengeCustomerApi.Application.Mappers;

public static class CustomerDTOMapper
{
    public static Domain.Models.Customer MapToEntity(this CreateCustomerRequestDTO dto)
        => Domain.Models.Customer.Create(dto.Name, new Cpf(dto.Cpf), dto.State);

    public static FindCustomerResponseDTO MapToFindDto(this Customer customer)
        => new()
        {
            Name = customer.Name,
            State = customer.State,
            Cpf = customer.Cpf.Value.ToString(@"000\.000\.000\-00"),
            CreatedAt = customer.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
            LastModifiedAt = customer.LastModifiedAt == null ? string.Empty : customer.LastModifiedAt?.ToString("dd/MM/yyyy HH:mm:ss")
        };
    
    public static IEnumerable<ListCustomersResponseDTO> MapToListDto(this IEnumerable<Customer> customers)
    {
        foreach (var customer in customers)
            yield return new()
            {
                Name = customer.Name,
                Cpf = customer.Cpf.Value.ToString()
            };
    }
}