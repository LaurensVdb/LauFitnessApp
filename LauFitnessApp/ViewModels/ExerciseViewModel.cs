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

        IExerciseRepository repository;
        IMapper mapper;
        ShowValidatorDisplay showValidatorDisplay;
        ExerciseValidator validator;
        public ExerciseViewModel(IExerciseRepository repository, IMapper mapper, ShowValidatorDisplay showValidatorDisplay, ExerciseValidator validator)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.showValidatorDisplay = showValidatorDisplay;
            this.validator = validator;
            exercise = new ExerciseDTO();
            exercises = new ObservableCollection<ExerciseDTO>();


        }

        public async Task GetExercises()
        {
            List<Exercise> result = await repository.GetExercises();
            Exercises = mapper.Map<ObservableCollection<ExerciseDTO>>(result);

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

                exerciseDB.BodyPart = Exercise.Bodypart;
                exerciseDB.Name = Exercise.Name;
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

            var result = await validator.ValidateAsync(exercise, options => options.IncludeRuleSets("Delete"));
            if (result.IsValid)
            {
                repository.RemoveExercise(exercise.Id);
                await repository.Save();
                Exercises.Remove(exercise);
            }
            else
            {
                await showValidatorDisplay.ShowValidationAsync(result, "exercises");
            }

        }
    }
}
