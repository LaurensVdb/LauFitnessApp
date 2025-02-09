using DataAcces.Repositories;
using FluentValidation;
using LauFitnessApp.Models;

namespace LauFitnessApp.Validators
{
    public class ExerciseValidator : AbstractValidator<ExerciseDTO>
    {
        private IExerciseRepository _repository;
        public ExerciseValidator(IExerciseRepository repository)
        {
            _repository = repository;
            RuleFor(p => p.Bodypart).NotEmpty().WithMessage("Bodypart is required!");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Exercise name is required!");
            RuleSet("Delete", () =>
            {
                RuleFor(p => p.Id).MustAsync(NotHaveWorkoutSets).WithMessage("This exercise is used in a workoutset!");
            });
        }

        private async Task<bool> NotHaveWorkoutSets(int id, CancellationToken token)
        {
            var result = await _repository.DoesExerciseHaveWorkoutSet(id);
            return !result;
        }


    }
}
