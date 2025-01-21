using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauFitnessApp.Models
{
    public partial class WorkoutSetDTO: ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string name = "";

        [ObservableProperty]
        public int reps;

        [ObservableProperty]
        public double weight;

        [ObservableProperty]
        public ExerciseDTO exercise;


    }
}
