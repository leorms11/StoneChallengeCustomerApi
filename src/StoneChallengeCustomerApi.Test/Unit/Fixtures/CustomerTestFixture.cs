using Bogus;
using Bogus.Extensions.Brazil;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Test.Unit.Fixtures;

[CollectionDefinition(nameof(CustomerCollection))]
public class CustomerCollection : ICollectionFixture<CustomerTestFixture> { }

public class CustomerTestFixture : IDisposable
{
    public Customer GenerateValidCustomer(Cpf? cpf = null)
    {
        var faker = new Faker();
        
        return Customer.Create(
            faker.Person.FullName,
            cpf ?? GenerateValidCpf(),
            "SP");
    }
    
    public Customer GenerateInvalidCustomer()
    {
        var faker = new Faker();
        
        return Customer.Create(
            faker.Person.FullName,
            "wrong_cpf",
            "wrong_state");
    }
    
    public CreateCustomerRequestDTO GenerateValidCreateCustomerRequestDto()
    {
        var faker = new Faker();
        
        return new()
        {
            Name = faker.Person.FullName,
            Cpf = GenerateValidCpf().Value.ToString(),
            State = "SP"
        };
    }
    
    public Cpf GenerateValidCpf()
    {
        var isValid = false;

        Cpf cpf = new Cpf(string.Empty);
        
        while (!isValid)
        {
            cpf = new Cpf(new Faker().Person.Cpf());
            isValid = cpf.IsValid;
        }

        return cpf;
    }

    public CreateCustomerRequestDTO GenerateInvalidCreateCustomerRequestDto()
    {
        var faker = new Faker();
        
        return new()
        {
            Name = faker.Person.FullName,
            Cpf = "wrong_cpf",
            State = "wrong_state"
        };
    }

    public IOperation<Customer> GenerateSuccessResultFromCustomer()
        => Result.CreateSuccess(GenerateValidCustomer());

    public IOperation<Customer> GenerateFailedResultFromCustomer(IEnumerable<ResultErrorField> notifications)
        => Result.CreateFailure<Customer>(EErrorType.InvalidData, notifications);

    public void Dispose()
    {
    }
}