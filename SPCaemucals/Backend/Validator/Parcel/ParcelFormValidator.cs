using FluentValidation;
using FluentValidation.Validators;
using SPCaemucals.Backend.Dto.Model;

namespace SPCaemucals.Backend.Validator.Parcel;

public class ParcelFormValidator:AbstractValidator<ParcelForm>
{
    public ParcelFormValidator()
    {
        RuleFor(x => x.Sender).SetValidator(new SenderDetailsValidator());
    }
}

public class SenderDetailsValidator : AbstractValidator<SenderDetails>
{
    public SenderDetailsValidator()
    {
        // RuleFor(sender => sender.Firstname).NotEmpty().WithMessage("Firstname cannot be empty")
        //     .Must(name => name != "string").WithMessage("Firstname cannot be 'string'");
        //
        // RuleFor(sender => sender.Lastname).NotEmpty().WithMessage("Lastname cannot be empty")
        //     .Must(lastname => lastname != "string").WithMessage("Lastname cannot be 'string'");
        //
        //
        // RuleFor(sender => sender.PhoneNo).NotEmpty().WithMessage("Phone number cannot be empty")
        //     .Must(phoneNo=>phoneNo != "string").WithMessage("Phone no cannot be 'string");
        //
    }
}