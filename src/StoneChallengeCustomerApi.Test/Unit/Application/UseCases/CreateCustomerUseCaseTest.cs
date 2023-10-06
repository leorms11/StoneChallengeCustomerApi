using Microsoft.Extensions.Logging;
using Moq;
using StoneChallengeCustomerApi.Application.UseCases.Customers;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Interfaces.Services;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Test.Unit.Fixtures;

namespace StoneChallengeCustomerApi.Test.Unit.Application.UseCases;

[Collection(nameof(CustomerCollection))]
public class CreateCustomerUseCaseTest
{
    private readonly CustomerTestFixture _fixture;


    public CreateCustomerUseCaseTest(CustomerTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Create a Valid Customer")]
    public async Task ItShouldBeAbleToCreateACustomer()
    {
        // Arrange
        var arr = _fixture.GenerateValidCreateCustomerRequestDto();
        var mockService = new Mock<ICustomerService>();
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<CreateCustomerUseCase>>();

        mockService.Setup(x => x.CreateAsync(It.IsAny<Customer>()))
            .ReturnsAsync(() => _fixture.GenerateSuccessResultFromCustomer());

        var sut = new CreateCustomerUseCase(mockLogger.Object, mockRepo.Object, mockService.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<SuccessOperation>(result);
    }

    [Fact(DisplayName = "Create a Customer With Invalid Dto")]
    public async Task ItShouldNotBeAbleToCreateACustomerWithAInvalidRequestDto()
    {
        // Arrange
        var arr = _fixture.GenerateInvalidCreateCustomerRequestDto();
        var mockService = new Mock<ICustomerService>();
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<CreateCustomerUseCase>>();

        var sut = new CreateCustomerUseCase(mockLogger.Object, mockRepo.Object, mockService.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<FailedOperation>(result);
    }

    [Fact(DisplayName = "Create an Invalid Customer")]
    public async Task ItShouldNotBeAbleToCreateACustomerWhenEntityIsInvalid()
    {
        // Arrange
        var arr = _fixture.GenerateValidCreateCustomerRequestDto();
        var arr2 = _fixture.GenerateInvalidCustomer();
        var mockService = new Mock<ICustomerService>();
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<CreateCustomerUseCase>>();

        mockService.Setup(x => x.CreateAsync(It.IsAny<Customer>()))
            .ReturnsAsync(() => _fixture.GenerateFailedResultFromCustomer(arr2.Notifications));

        var sut = new CreateCustomerUseCase(mockLogger.Object, mockRepo.Object, mockService.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<FailedOperation>(result);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors.Fields);
    }
    
    [Fact(DisplayName = "Create a Customer When Cpf Already Exists")]
    public async Task ItShouldNotBeAbleToCreateACustomerWhenCpfAlreadyExists()
    {
        // Arrange
        var arr = _fixture.GenerateValidCreateCustomerRequestDto();
        var arr2 = _fixture.GenerateInvalidCustomer();
        var mockService = new Mock<ICustomerService>();
        var mockRepo = new Mock<ICustomersRepository>();
        var mockLogger = new Mock<ILogger<CreateCustomerUseCase>>();

        mockRepo.Setup(x => x.GetByCpfAsync(It.IsAny<long>()))
            .ReturnsAsync(() => arr2);

        var sut = new CreateCustomerUseCase(mockLogger.Object, mockRepo.Object, mockService.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);

        // Assert
        Assert.IsType<FailedOperation>(result);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors.Fields);
    }
}