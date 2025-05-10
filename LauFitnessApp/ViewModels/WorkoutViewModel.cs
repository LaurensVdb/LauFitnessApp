using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAcces.Entities;
using DataAcces.Repositories;
using ForgeMapperLibrary;
using ForgeMapperLibrary.Interfaces;
using LauFitnessApp.Models;
using LauFitnessApp.Validators;
using System.Collections.ObjectModel;

namespace LauFitnessApp.ViewModels
{
    public partial class WorkoutViewModel : ObservableObject, IQueryAttributable
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

        IShowValidatorDisplay showValidatorDisplay;
        WorkoutValidator validator;
        WorkoutSetValidator workoutSetValidator;
        IForgeMapper mapper;
        public WorkoutViewModel(IExerciseRepository exerciseRepository, IWorkoutRepository workoutRepository, IShowValidatorDisplay showValidatorDisplay, WorkoutValidator validator, WorkoutSetValidator workoutSetValidator, IForgeMapper forgeMapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.workoutRepository = workoutRepository;

            this.showValidatorDisplay = showValidatorDisplay;
            this.workoutSetValidator = workoutSetValidator;
            this.validator = validator;
            this.mapper = forgeMapper;
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
            Exercises = result.MapCollection<ObservableCollection<ExerciseDTO>>();
        }

        [RelayCommand]
        async Task AddWorkoutSet()
        {
            var result = workoutSetValidator.Validate(SelectedWorkoutSet);
            if (result.IsValid)
            {
                WorkoutSets.Add(SelectedWorkoutSet);
                SelectedWorkoutSet = new WorkoutSetDTO();
                SelectedWorkoutSet.Exercise = new ExerciseDTO();
            }
            else
            {
                await showValidatorDisplay.ShowValidationAsync(result, "workout");
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
            var workoutSetsDb = WorkoutSets.MapCollection<ObservableCollection<WorkoutSet>>();


            if (isresult)
            {
                var validationresult = validator.Validate(SelectedWorkout);

                if (validationresult.IsValid)
                {
                    Workout? workoutDb = mapper.CreateObject<Workout>(SelectedWorkout);
                    if (SelectedWorkout.Id > 0)
                    {
                        workoutDb = await workoutRepository.GetWorkout(SelectedWorkout.Id);

                    }
                    if (workoutDb != null)
                    {
                        workoutDb.WorkoutSets = workoutSetsDb;

                        if (workoutDb.Id <= 0)
                        {

                            await workoutRepository.AddWorkout(workoutDb);

                        }
                        else
                        {

                            workoutRepository.UpdateWorkout(workoutDb);
                        }
                        await workoutRepository.Save();
                        SelectedWorkout.Id = workoutDb.Id;
                        await Shell.Current.GoToAsync(nameof(MainPage));
                    }

                }
                else
                {
                    await showValidatorDisplay.ShowValidationAsync(validationresult, "workout");
                }


            }
        }




        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            this.SelectedWorkout = (WorkoutDTO)query["workout"];
            this.WorkoutSets = new ObservableCollection<WorkoutSetDTO>(this.SelectedWorkout.WorkoutSets);
        }
    }
}
