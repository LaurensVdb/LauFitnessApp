using DataAcces.DatabaseConfig;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;

namespace LauFitnessApp
{
    public partial class App : Application
    {
        public App(DatabaseContext databaseContext)
        {
            databaseContext.Database.Migrate();
            InitializeComponent();
  
          
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
