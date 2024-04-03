using FluentValidation;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Models;

namespace SPCaemucals.Backend.Validator;

public class JobModelValidator : AbstractValidator<JobModel>
{
    public JobModelValidator()
    {
        RuleFor(x => x.JobServiceId).GreaterThan(0);
        RuleFor(x => x.JobTypeId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.MC).GreaterThan(0);
        
    }
}