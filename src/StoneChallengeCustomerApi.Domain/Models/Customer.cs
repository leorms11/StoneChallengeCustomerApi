using StoneChallengeCustomerApi.Domain.Validators;
using StoneChallengeCustomerApi.Domain.ValueObjects;

namespace StoneChallengeCustomerApi.Domain.Models;

public class Customer : BaseModel
{
    protected Customer() { }

    public Customer(string name, Cpf cpf, string state)
    {
        Name = name;
        Cpf = cpf;
        State = state;

        Validate(this, CustomerValidator.Instance);
    }

    public static Customer Create(string name, Cpf cpf, string state)
        => new(name, cpf, state);

    public string Name { get; private set; }
    public Cpf Cpf { get; private set; }
    public string State { get; private set; }
}