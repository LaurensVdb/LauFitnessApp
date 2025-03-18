using CommunityToolkit.Mvvm.ComponentModel;

namespace LauFitnessApp.Models
{
    public partial class WorkoutDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string workoutName = "";

        [ObservableProperty]
        public DateTime date;

        [ObservableProperty]
        public TimeSpan time;

        [ObservableProperty]
        public bool isVisible = false;

        [ObservableProperty]
        public List<WorkoutSetDTO> workoutSets = new List<WorkoutSetDTO>();
    }
}
