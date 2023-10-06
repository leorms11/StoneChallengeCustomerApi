using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<IOperation<Customer>> CreateAsync(Customer customer);
}