using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Application.UseCases.Customers;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Interfaces.Services;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Test.Unit.Fixtures;

namespace StoneChallengeCustomerApi.Test.Unit.Application.UseCases;

[Collection(nameof(CustomerCollection))]
public class ListCustomersUseCaseTest
{
    private readonly CustomerTestFixture _fixture;


    public ListCustomersUseCaseTest(CustomerTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "List customers")]
    public async Task ItShouldBeAbleToListAllCustomers()
    {
        // Arrange
        var arr = new List<Customer>()
        {
            _fixture.GenerateValidCustomer(),
            _fixture.GenerateValidCustomer(),
            _fixture.GenerateValidCustomer(),
            _fixture.GenerateValidCustomer(),
        };
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<ListCustomersUseCase>>();

        mockRepo.Setup(x => x.ListAsync())
            .ReturnsAsync(() => arr);

        var sut = new ListCustomersUseCase(mockLogger.Object, mockRepo.Object);

        // Act
        var result = await sut.ExecuteAsync();

        // Assert
        Assert.IsType<SuccessOperation<IEnumerable<ListCustomersResponseDTO>>>(result);
        Assert.NotEmpty(result.Data);
    }
}