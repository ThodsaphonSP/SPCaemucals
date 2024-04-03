using FluentValidation;

namespace SPCaemucals.Backend.Validator;

public class ParcelValidator:AbstractValidator<Data.Identities.Parcel>
{
    public ParcelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.ParcelStatus).IsInEnum();
    }
}