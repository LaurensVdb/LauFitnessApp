using LauFitnessApp.ViewModels;

namespace LauFitnessApp;

public partial class ExercisePage : ContentPage
{
    ExerciseViewModel exerciseViewModel;
    public ExercisePage(ExerciseViewModel exerciseViewModel)
    {
        this.exerciseViewModel = exerciseViewModel;
        BindingContext = exerciseViewModel;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        await exerciseViewModel.GetExercises();
    }
}