using DataAcces.DatabaseConfig;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private DatabaseContext databaseContext;
        public ExerciseRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Task<List<Exercise>> GetExercises()
        {
            return databaseContext.Exercises.ToListAsync();
        }

        public ValueTask<EntityEntry<Exercise>> AddExercise(Exercise exercise)
        {
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

            if(exercise != null)
                databaseContext.Exercises.Remove(exercise);
        }
        public Task Save()
        {
            return databaseContext.SaveChangesAsync();
        }
    }
}
