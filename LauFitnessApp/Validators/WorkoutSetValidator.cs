using FluentValidation;
using LauFitnessApp.Models;

namespace LauFitnessApp.Validators
{
    public class WorkoutSetValidator : AbstractValidator<WorkoutSetDTO>
    {
        public WorkoutSetValidator()
        {
            RuleFor(p => p.Exercise).NotNull().WithMessage("Exercise is required!");
            RuleFor(p => p.Weight).GreaterThan(0).WithMessage("Weight is required!");
            RuleFor(p => p.Reps).GreaterThan(0).WithMessage("Reps are required!");
        }
    }
}
