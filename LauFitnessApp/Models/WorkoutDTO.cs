using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauFitnessApp.Models
{
    public partial class WorkoutDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string workoutName = "";

        [ObservableProperty]
        public DateTime date;

        [ObservableProperty]
        public TimeSpan time;
    }
}
