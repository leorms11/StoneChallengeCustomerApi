using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;

public interface IListCustomersUseCase
{
    Task<IOperation<IEnumerable<ListCustomersResponseDTO>>> ExecuteAsync();
}