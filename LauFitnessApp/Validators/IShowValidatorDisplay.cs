using FluentValidation.Results;

namespace LauFitnessApp.Validators
{
    public interface IShowValidatorDisplay
    {
        Task ShowValidationAsync(ValidationResult validationResult, string title);
        Task<bool> ShowValidationWithConfirmation(ValidationResult validationResult, string title, string additionalMessage);
    }
}