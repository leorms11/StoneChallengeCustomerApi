using Bogus;
using Bogus.Extensions.Brazil;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Test.Integration.Config;

namespace StoneChallengeCustomerApi.Test.Integration.Fixtures;

[CollectionDefinition(nameof(IntegrationTestFixtureCollection))]
public class IntegrationTestFixtureCollection : ICollectionFixture<IntegrationTestFixture> { }

public class IntegrationTestFixture : IDisposable
{
    private readonly CustomerAppFactory Factory;
    public readonly HttpClient Client;

    public IntegrationTestFixture()
    {
        Factory = new CustomerAppFactory();
        Client = Factory.CreateClient();
    }

    public async Task<HttpResponseMessage> GetSwagger()
        => await Client.GetAsync("/swagger/v1/swagger.json");

    public CreateCustomerRequestDTO GenerateValidCreateRequestDto()
    {
        var faker = new Faker();
        return new()
        {
            Name = faker.Person.FullName,
            State = "SP",
            Cpf = GenerateValidCpf().Value.ToString()
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

    public void Dispose()
    {
        Client.Dispose();
        Factory?.Dispose();
    }
}