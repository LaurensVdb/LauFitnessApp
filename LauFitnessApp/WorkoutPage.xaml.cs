using LauFitnessApp.ViewModels;

namespace LauFitnessApp;

public partial class WorkoutPage : ContentPage
{
    WorkoutViewModel workoutViewModel;
    public WorkoutPage(WorkoutViewModel workoutViewModel)
	{
        this.workoutViewModel = workoutViewModel;
		this.BindingContext = workoutViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {

        await workoutViewModel.GetExercises();
    }
}