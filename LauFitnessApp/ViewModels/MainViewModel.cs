using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAcces.Entities;
using DataAcces.Repositories;
using ForgeMapperLibrary;
using LauFitnessApp.Models;
using System.Collections.ObjectModel;

namespace LauFitnessApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        IWorkoutRepository workoutRepository;


        [ObservableProperty]
        ObservableCollection<WorkoutDTO> workouts;
        public MainViewModel(IWorkoutRepository workoutRepository)
        {
            this.workoutRepository = workoutRepository;

            Workouts = new ObservableCollection<WorkoutDTO>();

        }

        [RelayCommand]
        public void ClickWorkout(WorkoutDTO workout)
        {
            workout.IsVisible = !workout.IsVisible;
        }

        public async Task GetWorkouts()
        {
            List<Workout> result = await workoutRepository.GetWorkoutsWithWorkoutSets();
            Workouts = result.MapCollection<ObservableCollection<WorkoutDTO>>();
        }
        [RelayCommand]
        async Task ManageExercises()
        {
            await Shell.Current.GoToAsync(nameof(ExercisePage));
        }


        [RelayCommand]
        public async Task EditWorkout(WorkoutDTO workout)
        {
            workout.IsVisible = !workout.IsVisible;
            var navigationParameter = new Dictionary<string, object>
            {
                { "workout", workout }
            };
            await Shell.Current.GoToAsync(nameof(WorkoutPage), navigationParameter);
            //Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items.FirstOrDefault(tab => tab.Title == "Workout");
        }

        [RelayCommand]
        async Task StartWorkout()
        {
            await Shell.Current.GoToAsync(nameof(WorkoutPage));
            Shell.Current.CurrentItem = Shell.Current.Items.FirstOrDefault(tab => tab.Title == "WorkoutPage");
        }
    }
}
