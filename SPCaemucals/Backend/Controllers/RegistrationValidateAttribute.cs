using System.ComponentModel.DataAnnotations;

namespace SPCaemucals.Backend.Controllers;

public class RegistrationValidateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (context.ObjectInstance is UserRegistrationRequest userContactModel)
        {
            if (string.IsNullOrWhiteSpace(userContactModel.Email) && string.IsNullOrWhiteSpace(userContactModel.PhoneNumber))
            {
                return new ValidationResult("Either email or phone number must be provided.");
            }

            if (userContactModel.PhoneNumber.Length < 10)
            {
                return new ValidationResult("Phone number must have length >= 10");
            }

            if (!userContactModel.RoleName.Any())
            {
                return new ValidationResult("Role must have one or more");
            }

            return ValidationResult.Success;
        }
      
        return new ValidationResult("Invalid object.");
    }
}