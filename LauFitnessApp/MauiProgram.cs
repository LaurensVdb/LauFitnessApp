using CommunityToolkit.Maui;
using DataAcces.DatabaseConfig;
using DataAcces.Repositories;
using LauFitnessApp.MapperProfiles;
using LauFitnessApp.Validators;
using LauFitnessApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
namespace LauFitnessApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                }).UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                });


            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, "exercise.db");
                options.UseSqlite($"Filename={path}");

            }, ServiceLifetime.Singleton);

            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddTransient<IWorkoutRepository, WorkoutRepository>();

            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<ExerciseViewModel>();
            builder.Services.AddTransient<ExercisePage>();

            builder.Services.AddTransient<WorkoutViewModel>();
            builder.Services.AddTransient<WorkoutPage>();
            builder.Services.AddTransient<ShowValidatorDisplay>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
