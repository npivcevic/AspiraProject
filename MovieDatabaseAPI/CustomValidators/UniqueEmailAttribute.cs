using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.CustomValidators;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DataContext? context = (DataContext?)validationContext.GetService(typeof(DataContext));
        if (context == null)
        {
            throw new InvalidOperationException("Unable to validate email uniqueness. Data context is not found.");
        }
        
        var email = value as string;
        
        var userId = GetUserId(validationContext);

        var user = context.Users.AsNoTracking().FirstOrDefault(u => u.Email == email && u.Id != userId);

        if (user != null)
        {
            return new ValidationResult("Email is already in use.");
        }

        return ValidationResult.Success;
    }

    private static int GetUserId(ValidationContext validationContext)
    {
        var idProperty = validationContext.ObjectType.GetProperty("Id");
        if (idProperty == null)
        {
            return 0;
        }
        return (int)(idProperty.GetValue(validationContext.ObjectInstance, null) ?? 0);
    }
}