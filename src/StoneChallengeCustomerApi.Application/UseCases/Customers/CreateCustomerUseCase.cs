using Microsoft.Extensions.Logging;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Application.Mappers;
using StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;
using StoneChallengeCustomerApi.Application.Validators.Customers;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Interfaces.Services;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Constants;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Application.UseCases.Customers;

public class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ILogger<CreateCustomerUseCase> _logger;
    private readonly ICustomersRepository _customersRepository;
    private readonly ICustomerService _customerService;

    public CreateCustomerUseCase(
        ILogger<CreateCustomerUseCase> logger, 
        ICustomersRepository customersRepository, 
        ICustomerService customerService)
    {
        _logger = logger;
        _customersRepository = customersRepository;
        _customerService = customerService;
    }

    public async Task<IOperation> ExecuteAsync(CreateCustomerRequestDTO dto)
    {
        _logger.LogInformation($"Iniciando cadastro de um novo cliente, cpf: {dto.Cpf}");
        
        dto.Validate(dto, CreateCustomerValidator.Instance);

        if (!dto.IsValid)
            return Result.CreateFailure(EErrorType.InvalidData, dto.Notifications);

        var newCustomer = dto.MapToEntity();
        
        var customerAlreadyExists = await _customersRepository.GetByCpfAsync(newCustomer.Cpf.Value);

        if (customerAlreadyExists is not null)
            return Result.CreateFailure(
                EErrorType.CpfAlreadyExists, 
                new List<ResultErrorField>()
                {
                    new ResultErrorField(nameof(newCustomer.Cpf).ToLower(), Constants.AlreadyExistsField("CPF"))
                });

        var result = await _customerService.CreateAsync(newCustomer);

        if (result is FailedOperation<Customer>)
            return Result.CreateFailure(EErrorType.InvalidData, result.Errors);
        
        _logger.LogInformation($"Cliente {result.Data.Name} #{result.Data.Cpf.Value}# cadastrado com sucesso.");

        return Result.CreateSuccess();
    }
}