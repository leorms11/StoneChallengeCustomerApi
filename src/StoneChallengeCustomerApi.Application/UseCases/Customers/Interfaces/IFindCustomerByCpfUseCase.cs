using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;

public interface IFindCustomerByCpfUseCase
{
    Task<IOperation<FindCustomerResponseDTO>> ExecuteAsync(Cpf cpf);
}