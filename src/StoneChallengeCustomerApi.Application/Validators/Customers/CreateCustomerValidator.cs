using FluentValidation;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Domain.Validators;
using StoneChallengeCustomerApi.Domain.ValueObjects;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Constants;

namespace StoneChallengeCustomerApi.Application.Validators.Customers;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequestDTO>
{
    public static readonly CreateCustomerValidator Instance = new();

    public CreateCustomerValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .MinimumLength(3)
            .WithMessage(Constants.MinLengthRequiredToString("nome", 3))
            .MaximumLength(50)
            .WithMessage(Constants.MaxLengthRequiredToString("nome", 50));

        RuleFor(dto => new Cpf(dto.Cpf))
            .SetValidator(CpfValidator.Instance);
        
        RuleFor(dto => dto.State)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .Length(2)
            .WithMessage(Constants.LengthRequiredToString("estado", 2));
    }
}