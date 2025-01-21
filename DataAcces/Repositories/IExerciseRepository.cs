using DataAcces.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAcces.Repositories
{
    public interface IExerciseRepository
    {
        ValueTask<EntityEntry<Exercise>> AddExercise(Exercise exercise);
        Task<List<Exercise>> GetExercises();
        Task Save();
        void UpdateExercise(Exercise exercise);
        void RemoveExercise(int id);
    }
}