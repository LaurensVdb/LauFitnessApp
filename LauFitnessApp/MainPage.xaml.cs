using LauFitnessApp.ViewModels;

namespace LauFitnessApp
{
    public partial class MainPage : ContentPage
    {

        MainViewModel mainViewModel;
        public MainPage(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            BindingContext = mainViewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await mainViewModel.GetWorkouts();
            base.OnAppearing();

        }

    }

}
