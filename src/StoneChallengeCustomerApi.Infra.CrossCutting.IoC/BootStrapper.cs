using Microsoft.Extensions.DependencyInjection;
using StoneChallengeCustomerApi.Application.UseCases.Customers;
using StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Interfaces.Services;
using StoneChallengeCustomerApi.Domain.Services;
using StoneChallengeCustomerApi.Infra.Persistence.Repositories;

namespace StoneChallengeCustomerApi.Infra.CrossCutting.IoC;

public static class BootStrapper
{
    public static IServiceProvider ServiceProvider = null;

    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .RegisterRepositories()
            .RegisterServicesAndUseCases();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomersRepository, CustomerRepository>();
        return serviceCollection;
    }

    private static IServiceCollection RegisterServicesAndUseCases(this IServiceCollection serviceCollection)
    {
        #region Application UseCases

        serviceCollection.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        serviceCollection.AddScoped<IFindCustomerByCpfUseCase, FindCustomerByCpfUseCase>();
        serviceCollection.AddScoped<IListCustomersUseCase, ListCustomersUseCase>();

        #endregion

        #region Domain Services

        serviceCollection.AddScoped<ICustomerService, CustomerService>();

        #endregion

        return serviceCollection;
    }

    public static T GetInstance<T>()
    {
        if (ServiceProvider is null)
            throw new InvalidOperationException("Os services não foram registrados.");

        return (T)ServiceProvider.GetService(typeof(T));
    }
}