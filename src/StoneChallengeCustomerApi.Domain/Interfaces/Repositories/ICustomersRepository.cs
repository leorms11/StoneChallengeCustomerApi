using StoneChallengeCustomerApi.Domain.Models;

namespace StoneChallengeCustomerApi.Domain.Interfaces.Repositories;

public interface ICustomersRepository
{
    Task<Customer?> GetByCpfAsync(long cpf);
    Task<Customer> CreateAsync(Customer customer);
    Task<IEnumerable<Customer>> ListAsync();
}