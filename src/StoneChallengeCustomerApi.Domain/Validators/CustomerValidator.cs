using FluentValidation;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Constants;

namespace StoneChallengeCustomerApi.Domain.Validators;

public class CustomerValidator : AbstractValidator<Models.Customer>
{
    public static readonly CustomerValidator Instance = new();

    public CustomerValidator()
    {
        RuleFor(customer => customer)
            .NotNull()
            .WithMessage(Constants.InvalidField(nameof(Models.Customer.Cpf).ToLower()))
            .Must(customer => customer.Cpf.Value != 0)
            .WithMessage(Constants.InvalidField(nameof(Models.Customer.Cpf).ToLower()));
        
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .MinimumLength(3)
            .WithMessage(Constants.MinLengthRequiredToString("nome", 3))
            .MaximumLength(50)
            .WithMessage(Constants.MaxLengthRequiredToString("nome", 50));
        
        RuleFor(dto => dto.State)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .Length(2)
            .WithMessage(Constants.LengthRequiredToString("estado", 2));
    }
}