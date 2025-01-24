using FluentValidation;
using LauFitnessApp.Models;

namespace LauFitnessApp.Validators
{
    public class WorkoutValidator : AbstractValidator<WorkoutDTO>
    {
        public WorkoutValidator()
        {
            RuleFor(p => p.WorkoutName).NotEmpty().WithMessage("Workout name is required!");
            RuleFor(p => p.Date).NotEmpty().WithMessage("Date is required!");
            RuleFor(p => p.Time).NotEmpty().WithMessage("Time is required!");
        }
    }
}
