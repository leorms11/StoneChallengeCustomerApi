using Microsoft.Extensions.Logging;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Application.Mappers;
using StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Interfaces.Services;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Application.UseCases.Customers;

public class FindCustomerByCpfUseCase : IFindCustomerByCpfUseCase
{
    private readonly ILogger<FindCustomerByCpfUseCase> _logger;
    private readonly ICustomersRepository _customersRepository;

    public FindCustomerByCpfUseCase(
        ILogger<FindCustomerByCpfUseCase> logger, 
        ICustomersRepository customersRepository)
    {
        _logger = logger;
        _customersRepository = customersRepository;
    }

    public async Task<IOperation<FindCustomerResponseDTO>> ExecuteAsync(Cpf cpf)
    {
        _logger.LogInformation($"Buscando cliente pelo cpf: {cpf.Value}");
        
        if (!cpf.IsValid)
            return Result.CreateFailure<FindCustomerResponseDTO>(EErrorType.InvalidCpf);

        var customer = await _customersRepository.GetByCpfAsync(cpf.Value);

        if (customer is null)
            return Result.CreateFailure<FindCustomerResponseDTO>(EErrorType.CustomerNotFound);

        return Result.CreateSuccess(customer.MapToFindDto());
    }
}