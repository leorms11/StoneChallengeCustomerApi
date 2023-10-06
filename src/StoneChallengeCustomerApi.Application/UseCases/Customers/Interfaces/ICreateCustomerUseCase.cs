using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;

public interface ICreateCustomerUseCase
{
    Task<IOperation> ExecuteAsync(CreateCustomerRequestDTO dto);
}