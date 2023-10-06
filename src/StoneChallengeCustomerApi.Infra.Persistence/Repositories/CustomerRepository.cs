using Microsoft.EntityFrameworkCore;
using StoneChallengeCustomerApi.Domain.Interfaces.Repositories;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Infra.Persistence.Context;

namespace StoneChallengeCustomerApi.Infra.Persistence.Repositories;

public class CustomerRepository : ICustomersRepository
{
    private readonly PostgreSqlDbContext _dbContext;

    public CustomerRepository(PostgreSqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Models.Customer?> GetByCpfAsync(long cpf)
        => await _dbContext.Set<Domain.Models.Customer>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Cpf.Value == cpf);

    public async Task<Customer> CreateAsync(Customer customer)
    {
        var entityEntry = await _dbContext.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<Customer>> ListAsync()
        => await _dbContext.Set<Customer>()
            .AsNoTracking()
            .ToListAsync();

}