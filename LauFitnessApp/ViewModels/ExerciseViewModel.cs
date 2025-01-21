using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAcces.Entities;
using DataAcces.Repositories;
using LauFitnessApp.Models;
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
        public ExerciseViewModel(IExerciseRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
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
            Exercises.Add(Exercise);

            Exercise exerciseDB = new Exercise();

            exerciseDB.BodyPart = Exercise.Bodypart;
            exerciseDB.Name = Exercise.Name;
            await repository.AddExercise(exerciseDB);


            await repository.Save();
            Exercise = new ExerciseDTO();

        }

        [RelayCommand]
        async Task Delete(ExerciseDTO exercise)
        {

            repository.RemoveExercise(exercise.Id);
            await repository.Save();
            Exercises.Remove(exercise);

        }
    }
}
