using DataAcces.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAcces.Repositories
{
    public interface IExerciseRepository
    {
        ValueTask<EntityEntry<Exercise>> AddExercise(Exercise exercise);
        Task<List<Exercise>> GetExercises();

        Task<List<Bodypart>> GetBodyparts();
        Task Save();
        void UpdateExercise(Exercise exercise);
        void RemoveExercise(int id);
        Task<bool> DoesExerciseHaveWorkoutSet(int idExercise);
    }
}