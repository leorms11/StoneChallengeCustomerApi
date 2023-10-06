using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Interfaces.Services;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomersRepository _customersRepository;

    public CustomerService(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<IOperation<Customer>> CreateAsync(Models.Customer customer)
    {
        if (!customer.IsValid)
            return Result.CreateFailure<Customer>(EErrorType.InvalidData, customer.Notifications);

        var createdCustomer = await _customersRepository.CreateAsync(customer);
        return Result.CreateSuccess(customer);
    }
}