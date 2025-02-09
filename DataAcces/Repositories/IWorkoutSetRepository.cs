
namespace DataAcces.Repositories
{
    public interface IWorkoutSetRepository
    {
        Task RemoveWorkoutSetsFromExercise(int idExercise);
    }
}