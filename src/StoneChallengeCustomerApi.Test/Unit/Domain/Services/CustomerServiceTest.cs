using Moq;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Domain.Services;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Test.Unit.Fixtures;

namespace StoneChallengeCustomerApi.Test.Unit.Domain.Services;

[Collection(nameof(CustomerCollection))]
public class CustomerServiceTest
{
    private readonly CustomerTestFixture _fixture;


    public CustomerServiceTest(CustomerTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Create a Valid Customer")]
    public async Task ItShouldBeAbleToCreateABilling()
    {
        // Arrange
        var arr = _fixture.GenerateValidCustomer();
        var mockRepo = new Mock<ICustomersRepository>();

        mockRepo.Setup(x => x.CreateAsync(It.IsAny<Customer>()))
            .ReturnsAsync(() => arr);

        var sut = new CustomerService(mockRepo.Object);

        // Act
        var result = await sut.CreateAsync(arr);

        // Assert
        Assert.Equal(result.Data.Cpf.Value, arr.Cpf.Value);
    }

    [Fact(DisplayName = "Create a Invalid Billing")]
    public async Task ItShouldNotBeAbleToCreateABillingWithInvalidProps()
    {
        // Arrange
        var arr = _fixture.GenerateInvalidCustomer();
        var mockRepo = new Mock<ICustomersRepository>();

        var sut = new CustomerService(mockRepo.Object);

        // Act
        var result = await sut.CreateAsync(arr);

        // Assert
        Assert.IsType<FailedOperation<Customer>>(result);
    }
}