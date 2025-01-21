using CommunityToolkit.Mvvm.ComponentModel;

namespace LauFitnessApp.Models
{
    public partial class ExerciseDTO:ObservableObject
    {
        [ObservableProperty]
        public string name = "";

        [ObservableProperty]
        public string bodypart = "";
        [ObservableProperty]
        public int id;
    }
}
