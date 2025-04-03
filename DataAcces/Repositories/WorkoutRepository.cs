using DataAcces.DatabaseConfig;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAcces.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private DatabaseContext databaseContext;
        public WorkoutRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Task<Workout> GetWorkout(int id)
        {
            return databaseContext.Workouts.FirstAsync(p => p.Id == id);
        }


        public Task<List<Workout>> GetWorkoutsOnly()
        {
            return databaseContext.Workouts.ToListAsync();
        }

        public Task<List<Workout>> GetWorkoutsWithWorkoutSets()
        {
            return databaseContext.Workouts.Include(p => p.WorkoutSets).ThenInclude(ws => ws.Exercise).ToListAsync();
        }

        public ValueTask<EntityEntry<Workout>> AddWorkout(Workout workout)
        {
            foreach (var entry in workout.WorkoutSets)
            {
                var existingExercise = databaseContext.Exercises.First(e => e.Id == entry.Exercise.Id);
                entry.Exercise = existingExercise;
            }



            return databaseContext.Workouts.AddAsync(workout);
        }

        public void UpdateWorkout(Workout workout)
        {
            //databaseContext.Exercises.Entry(exercise).State= EntityState.Modified;
            //var existingWorkout=databaseContext.Workouts.Local.First(p=>p.Id==workout.Id);
            //existingWorkout = workout;
            foreach (var entry in workout.WorkoutSets)
            {
                var existingExercise = databaseContext.Exercises.First(e => e.Id == entry.Exercise.Id);
                entry.Exercise = existingExercise;
            }
            databaseContext.Workouts.Update(workout);


        }

        public void RemoveWorkout(int id)
        {

            var workout = databaseContext.Workouts.Find(id);

            if (workout != null)
                databaseContext.Workouts.Remove(workout);
        }
        public Task Save()
        {
            return databaseContext.SaveChangesAsync();
        }
    }
}
