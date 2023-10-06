using FluentValidation;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Constants;

namespace StoneChallengeCustomerApi.Domain.Validators;

public class CpfValidator : AbstractValidator<Cpf>
{
    public static readonly CpfValidator Instance = new();

    public CpfValidator()
    {
        RuleFor(cpf => cpf)
            .Must(cpf => cpf.IsValid)
            .WithMessage(Constants.InvalidField("CPF"));
        
        RuleFor(cpf => cpf.Value)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing);
    }
}