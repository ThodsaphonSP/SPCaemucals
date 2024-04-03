using FluentValidation;
using SPCaemucals.Backend.Models;

namespace SPCaemucals.Backend.Validator;

public class TrackingValidator:AbstractValidator<TrackingModel>
{
    public TrackingValidator()
    {
        RuleFor(x => x.ParcelId).GreaterThan(0);
        RuleFor(x => x.TrackNO).NotEmpty();
    }
}