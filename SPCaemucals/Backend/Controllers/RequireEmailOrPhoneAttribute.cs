using System.ComponentModel.DataAnnotations;

namespace SPCaemucals.Backend.Controllers;

public class RequireEmailOrPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        var userContactModel = context.ObjectInstance as UserRegistrationRequest;
         
        if (userContactModel != null)
        {
            if (string.IsNullOrWhiteSpace(userContactModel.Email) && string.IsNullOrWhiteSpace(userContactModel.PhoneNumber))
            {
                return new ValidationResult("Either email or phone number must be provided.");
            }

            return ValidationResult.Success;
        }
      
        return new ValidationResult("Invalid object.");
    }
}