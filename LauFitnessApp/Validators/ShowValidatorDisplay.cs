using FluentValidation.Results;
using System.Text;

namespace LauFitnessApp.Validators
{
    public class ShowValidatorDisplay
    {

        public ShowValidatorDisplay()
        {

        }

        public async Task ShowValidationAsync(ValidationResult validationResult, string title)
        {
            StringBuilder errorMessage = new StringBuilder();
            foreach (var error in validationResult.Errors.Select(p => p.ErrorMessage))
            {
                errorMessage.AppendLine(error);

            };
            await Shell.Current.DisplayAlert(title, errorMessage.ToString(), "Cancel");
        }
    }
}
