using DataAcces.DatabaseConfig;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Repositories
{
    public class WorkoutSetRepository : IWorkoutSetRepository
    {
        private DatabaseContext databaseContext;

        public WorkoutSetRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task RemoveWorkoutSetsFromExercise(int idExercise)
        {
            var workoutsets = await databaseContext.WorkoutSets.Include(p => p.Exercise).Where(p => p.Exercise.Id == idExercise).ToListAsync();
            if (workoutsets != null)
            {
                databaseContext.RemoveRange(workoutsets);
            }
        }
    }
}
