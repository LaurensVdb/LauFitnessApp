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
            RuleSet("Delete", () =>
            {
                RuleFor(p => p.Id).MustAsync(NotHaveWorkoutSets).WithMessage("De oefening heeft nog workoutsets!");
            });
        }

        private async Task<bool> NotHaveWorkoutSets(int id, CancellationToken token)
        {
            var result = await _repository.DoesExerciseHaveWorkoutSet(id);
            return !result;
        }


    }
}
