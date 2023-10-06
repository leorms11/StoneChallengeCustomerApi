using Microsoft.Extensions.Logging;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Application.Mappers;
using StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Application.UseCases.Customers;

public class ListCustomersUseCase : IListCustomersUseCase
{
    private readonly ILogger<ListCustomersUseCase> _logger;
    private readonly ICustomersRepository _customersRepository;

    public ListCustomersUseCase(
        ILogger<ListCustomersUseCase> logger, 
        ICustomersRepository customersRepository)
    {
        _logger = logger;
        _customersRepository = customersRepository;
    }

    public async Task<IOperation<IEnumerable<ListCustomersResponseDTO>>> ExecuteAsync()
    {
        var customers = await _customersRepository.ListAsync();

        return Result.CreateSuccess(customers.MapToListDto());
    }
}