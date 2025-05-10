using CommunityToolkit.Mvvm.ComponentModel;

namespace LauFitnessApp.Models
{
    public partial class ExerciseDTO : ObservableObject
    {
        [ObservableProperty]
        public string name = "";
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public BodypartDTO bodyPart;
    }
}
