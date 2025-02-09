using FluentValidation.Results;
using System.Text;

namespace LauFitnessApp.Validators
{
    public class ShowValidatorDisplay : IShowValidatorDisplay
    {

        public ShowValidatorDisplay()
        {

        }

        public Task ShowValidationAsync(ValidationResult validationResult, string title)
        {
            StringBuilder errorMessage = new StringBuilder();
            foreach (var error in validationResult.Errors.Select(p => p.ErrorMessage))
            {
                errorMessage.AppendLine(error);

            };
            return Shell.Current.DisplayAlert(title, errorMessage.ToString(), "Cancel");
        }

        public Task<bool> ShowValidationWithConfirmation(ValidationResult validationResult, string title, string additionalMessage)
        {

            var errorMessage = FormatErrorMessages(validationResult);
            errorMessage.AppendLine(additionalMessage);
            return Shell.Current.DisplayAlert(title, errorMessage.ToString(), "Yes", "No");

        }

        private StringBuilder FormatErrorMessages(ValidationResult result)
        {
            StringBuilder errorMessage = new StringBuilder();
            foreach (var error in result.Errors.Select(p => p.ErrorMessage))
            {
                errorMessage.AppendLine(error);

            };

            return errorMessage;
        }
    }
}
