using DataAcces.DatabaseConfig;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAcces.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private DatabaseContext databaseContext;
        public ExerciseRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Task<bool> DoesExerciseHaveWorkoutSet(int idExercise)
        {
            return databaseContext.WorkoutSets.Include(p => p.Exercise).AnyAsync(p => p.Exercise.Id == idExercise);
        }
        public Task<List<Exercise>> GetExercises()
        {
            return databaseContext.Exercises.ToListAsync();
        }

        public ValueTask<EntityEntry<Exercise>> AddExercise(Exercise exercise)
        {
            var bodypart = databaseContext.Bodyparts.Local.First(e => e.Id == exercise.BodyPart.Id);
            exercise.BodyPart = bodypart;
            return databaseContext.Exercises.AddAsync(exercise);
        }

        public void UpdateExercise(Exercise exercise)
        {
            //databaseContext.Exercises.Entry(exercise).State= EntityState.Modified;
            databaseContext.Exercises.Update(exercise);
        }

        public void RemoveExercise(int id)
        {

            var exercise = databaseContext.Exercises.Find(id);

            if (exercise != null)
                databaseContext.Exercises.Remove(exercise);
        }
        public Task Save()
        {
            return databaseContext.SaveChangesAsync();
        }

        public Task<List<Bodypart>> GetBodyparts()
        {
            return databaseContext.Bodyparts.ToListAsync();
        }
    }
}
