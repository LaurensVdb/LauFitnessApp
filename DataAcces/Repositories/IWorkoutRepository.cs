using DataAcces.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAcces.Repositories
{
    public interface IWorkoutRepository
    {
        ValueTask<EntityEntry<Workout>> AddWorkout(Workout workout);
        Task<List<Workout>> GetWorkoutsOnly();
        Task<List<Workout>> GetWorkoutsWithWorkoutSets();
        void RemoveExercise(int id);
        Task Save();
        void UpdateWorkout(Workout workout);

        Task<Workout> GetWorkout(int id);
    }
}