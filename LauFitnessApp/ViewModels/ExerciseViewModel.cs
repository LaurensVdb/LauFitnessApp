using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAcces.Entities;
using DataAcces.Repositories;
using FluentValidation;
using LauFitnessApp.Models;
using LauFitnessApp.Validators;
using System.Collections.ObjectModel;
namespace LauFitnessApp.ViewModels
{
    public partial class ExerciseViewModel : ObservableObject
    {
        [ObservableProperty]
        ExerciseDTO exercise;

        [ObservableProperty]
        ObservableCollection<ExerciseDTO> exercises;

        [ObservableProperty]
        ObservableCollection<BodypartDTO> bodyparts;


        IExerciseRepository repository;
        IMapper mapper;
        IShowValidatorDisplay showValidatorDisplay;
        ExerciseValidator validator;

        IWorkoutSetRepository workoutSetRepository;
        public ExerciseViewModel(IExerciseRepository repository, IWorkoutSetRepository workoutSetRepository, IMapper mapper, IShowValidatorDisplay showValidatorDisplay, ExerciseValidator validator)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.showValidatorDisplay = showValidatorDisplay;
            this.validator = validator;
            this.workoutSetRepository = workoutSetRepository;
            exercise = new ExerciseDTO();
            exercises = new ObservableCollection<ExerciseDTO>();
            bodyparts = new ObservableCollection<BodypartDTO>();


        }

        public async Task GetExercises()
        {
            List<Exercise> result = await repository.GetExercises();
            List<Bodypart> result2 = await repository.GetBodyparts();

            Exercises = mapper.Map<ObservableCollection<ExerciseDTO>>(result);
            Bodyparts = mapper.Map<ObservableCollection<BodypartDTO>>(result2);

            // var result2 = mapper.Map<List<ExerciseDTO>>(result);
            // var ex = result2.FirstOrDefault();
            // ex.Name = "loe";

            //var exdb = mapper.Map<Exercise>(ex);
            //repository.UpdateExercise(exdb);


        }

        [RelayCommand]
        async Task Add()
        {

            var result = validator.Validate(this.Exercise);
            if (result.IsValid)
            {
                Exercises.Add(Exercise);

                Exercise exerciseDB = new Exercise();

                //exerciseDB.BodyPart = Exercise.Bodypart;
                //exerciseDB.Name = Exercise.Name;

                mapper.Map(Exercise, exerciseDB);
                await repository.AddExercise(exerciseDB);


                await repository.Save();
                Exercise = new ExerciseDTO();
            }
            else
            {
                await showValidatorDisplay.ShowValidationAsync(result, "exercises");
            }

        }

        [RelayCommand]
        async Task Delete(ExerciseDTO exercise)
        {

            var validationresult = await validator.ValidateAsync(exercise, options => options.IncludeRuleSets("Delete"));
            if (validationresult.IsValid)
            {
                await RemoveExerciseAsync(exercise);
            }
            else
            {
                var resultConfirmation = await showValidatorDisplay.ShowValidationWithConfirmation(validationresult, "exercises", "Delete the workoutsets and the exercise?");
                if (resultConfirmation)
                {
                    await workoutSetRepository.RemoveWorkoutSetsFromExercise(exercise.Id);
                    await RemoveExerciseAsync(exercise);
                }
            }

        }



        private async Task RemoveExerciseAsync(ExerciseDTO exercise)
        {
            repository.RemoveExercise(exercise.Id);

            Exercises.Remove(exercise);
            await repository.Save();
        }

    }
}
