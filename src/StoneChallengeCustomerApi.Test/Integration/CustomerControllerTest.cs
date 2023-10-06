using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;
using StoneChallengeCustomerApi.Infra.CrossCutting.IoC;
using StoneChallengeCustomerApi.Test.Integration.Config;
using StoneChallengeCustomerApi.Test.Integration.Fixtures;

namespace StoneChallengeCustomerApi.Test.Integration;

[Collection(nameof(IntegrationTestFixtureCollection))]
public class BillingControllerTest
{
    private readonly IntegrationTestFixture _fixture;

    private readonly ICreateCustomerUseCase _createCustomerUseCase;
    private readonly IFindCustomerByCpfUseCase _findCustomerByCpfUseCase;
    private readonly IListCustomersUseCase _listCustomersUseCase;

    public BillingControllerTest(
        IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _createCustomerUseCase = BootStrapper.GetInstance<ICreateCustomerUseCase>();
        _findCustomerByCpfUseCase = BootStrapper.GetInstance<IFindCustomerByCpfUseCase>();
        _listCustomersUseCase = BootStrapper.GetInstance<IListCustomersUseCase>();
    }

    [Fact]
    public async Task ItShouldBeAbleToCreateACustomer()
    {
        var arr = _fixture.GenerateValidCreateRequestDto();
        var response = await _fixture.Client.PostAsJsonAsync("api/v1/customers", arr);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ItShouldBeAbleToFindACustomer()
    {
        var arr = _fixture.GenerateValidCreateRequestDto();
        var response = await _fixture.Client.PostAsJsonAsync("api/v1/customers", arr);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await _fixture.Client.GetAsync($"api/v1/customers/{arr.Cpf}");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task ItShouldBeAbleToListAllCustomers()
    {

        var response = await _fixture.Client.GetAsync("api/v1/customers");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}