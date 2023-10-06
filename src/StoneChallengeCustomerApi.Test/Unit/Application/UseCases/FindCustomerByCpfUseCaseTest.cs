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
public class FindCustomerByCpfUseCaseTest
{
    private readonly CustomerTestFixture _fixture;


    public FindCustomerByCpfUseCaseTest(CustomerTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Find a Customer")]
    public async Task ItShouldBeAbleToFindACustomer()
    {
        // Arrange
        var arr = _fixture.GenerateValidCpf();
        var arr2 = _fixture.GenerateValidCustomer(arr);
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<FindCustomerByCpfUseCase>>();

        mockRepo.Setup(x => x.GetByCpfAsync(It.IsAny<long>()))
            .ReturnsAsync(() => arr2);

        var sut = new FindCustomerByCpfUseCase(mockLogger.Object, mockRepo.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<SuccessOperation<FindCustomerResponseDTO>>(result);
        result.Data.Cpf.Should().Be(arr.Value.ToString(@"000\.000\.000\-00"));
    }

    [Fact(DisplayName = "Find a Customer passing an Invalid CPF")]
    public async Task ItShouldNotBeAbleToFindACustomerWhenPassingAnInvalidCpf()
    {
        // Arrange
        var arr = string.Empty;
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<FindCustomerByCpfUseCase>>();

        var sut = new FindCustomerByCpfUseCase(mockLogger.Object, mockRepo.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<FailedOperation<FindCustomerResponseDTO>>(result);
    }

    [Fact(DisplayName = "Find a Nonexistent Customer")]
    public async Task ItShouldNotBeAbleToFindACustomerWhenDoesNotExist()
    {
        // Arrange
        var arr = _fixture.GenerateValidCpf();
        var mockService = new Mock<ICustomerService>();
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<FindCustomerByCpfUseCase>>();

        var sut = new FindCustomerByCpfUseCase(mockLogger.Object, mockRepo.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<FailedOperation<FindCustomerResponseDTO>>(result);
        Assert.NotNull(result.Reason);
    }
}