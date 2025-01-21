using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAcces.Entities;
using DataAcces.Repositories;
using LauFitnessApp.Models;
using System.Collections.ObjectModel;

namespace LauFitnessApp.ViewModels
{
    public partial class WorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<ExerciseDTO> exercises;

        [ObservableProperty]
        ObservableCollection<WorkoutSetDTO> workoutSets;


        [ObservableProperty]
        WorkoutDTO selectedWorkout;

        [ObservableProperty]
        WorkoutSetDTO selectedWorkoutSet;

        IExerciseRepository exerciseRepository;
        IWorkoutRepository workoutRepository;
        IMapper mapper;

        public WorkoutViewModel(IExerciseRepository exerciseRepository, IWorkoutRepository workoutRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.workoutRepository = workoutRepository;
            this.mapper = mapper;

            Exercises = new ObservableCollection<ExerciseDTO>();

            SelectedWorkout = new WorkoutDTO();
            selectedWorkout.Date = DateTime.Today.Date;
            selectedWorkout.Time = DateTime.Now.TimeOfDay;
            SelectedWorkoutSet = new WorkoutSetDTO() { Exercise = new ExerciseDTO() };

            WorkoutSets = new ObservableCollection<WorkoutSetDTO>();

        }

        public async Task GetExercises()
        {
            List<Exercise> result = await exerciseRepository.GetExercises();
            Exercises = mapper.Map<ObservableCollection<ExerciseDTO>>(result);
        }

        [RelayCommand]
        void AddWorkoutSet()
        {
            if (SelectedWorkoutSet.Exercise != null)
            {
                WorkoutSets.Add(SelectedWorkoutSet);
                SelectedWorkoutSet = new WorkoutSetDTO();
                SelectedWorkoutSet.Exercise = new ExerciseDTO();
            }

        }

        [RelayCommand]
        public void DeleteWorkoutSet(WorkoutSetDTO workoutSet)
        {
            WorkoutSets.Remove(workoutSet);
        }

        [RelayCommand]
        async Task SaveWorkout()
        {
            var isresult = await Shell.Current.DisplayAlert("workout", "Are you done training?", "Yes", "No");

            if (isresult)
            {

                var workout = mapper.Map<Workout>(SelectedWorkout);
                var workoutSets = mapper.Map<ObservableCollection<WorkoutSet>>(WorkoutSets);

                if (workout != null && workoutSets != null)
                {
                    workout.WorkoutSets = workoutSets;
                    if (workout.Id <= 0)
                    {

                        await workoutRepository.AddWorkout(workout);

                    }

                    /*
                    else
                    {
                        var workoutdb = await workoutRepository.GetWorkout(SelectedWorkout.Id);
                        if (workoutdb != null)
                        {
                            mapper.Map(SelectedWorkout, workoutdb);

                            workoutRepository.UpdateWorkout(workoutdb);
                        }


                    }
                    */
                    await workoutRepository.Save();
                    SelectedWorkout.Id = workout.Id;
                }
            }

        }
    }
}
