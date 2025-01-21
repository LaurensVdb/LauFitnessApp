namespace LauFitnessApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            Routing.RegisterRoute(nameof(WorkoutPage), typeof(WorkoutPage));
        }
    }
}
