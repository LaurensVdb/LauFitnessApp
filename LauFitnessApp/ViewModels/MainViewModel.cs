using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAcces.Entities;
using DataAcces.Repositories;
using LauFitnessApp.Models;
using System.Collections.ObjectModel;

namespace LauFitnessApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        IWorkoutRepository workoutRepository;
        IMapper mapper;

        [ObservableProperty]
        ObservableCollection<WorkoutDTO> workouts;
        public MainViewModel(IWorkoutRepository workoutRepository, IMapper mapper)
        {
            this.workoutRepository = workoutRepository;
            this.mapper = mapper;
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
            Workouts = mapper.Map<ObservableCollection<WorkoutDTO>>(result);
        }
        [RelayCommand]
        async Task ManageExercises()
        {
            await Shell.Current.GoToAsync(nameof(ExercisePage));
        }

        [RelayCommand]
        async Task StartWorkout()
        {
            await Shell.Current.GoToAsync(nameof(WorkoutPage));
        }
    }
}
