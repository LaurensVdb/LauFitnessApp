using CommunityToolkit.Mvvm.ComponentModel;

namespace LauFitnessApp.Models
{
    public partial class BodypartDTO : ObservableObject
    {
        [ObservableProperty]
        public string name = "";

        [ObservableProperty]
        public int id;

    }
}
