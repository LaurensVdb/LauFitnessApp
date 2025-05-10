using CommunityToolkit.Maui;
using DataAcces.DatabaseConfig;
using DataAcces.Entities;
using DataAcces.Repositories;
using ForgeMapperLibrary;
using ForgeMapperLibrary.Interfaces;
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



            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, "laufitness2.db");
                options.UseSqlite($"Filename={path}").UseSeeding((context, _) =>
                {
                    var bodyparts = context.Set<Bodypart>().Count();
                    if (bodyparts <= 0)
                    {
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Chest" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Upper back" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Lower back" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Shoulders" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Chest" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Biceps" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Triceps" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Quadtriceps" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Hamstrings" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Calves" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Abs" });
                        context.Set<Bodypart>().Add(new Bodypart { Name = "Traps" });

                        context.SaveChanges();
                    }
                });

            }, ServiceLifetime.Singleton);

            builder.Services.AddScoped<IForgeMapper, ForgeMapper>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            builder.Services.AddTransient<IWorkoutSetRepository, WorkoutSetRepository>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<ExerciseViewModel>();
            builder.Services.AddTransient<ExercisePage>();

            builder.Services.AddTransient<WorkoutViewModel>();
            builder.Services.AddTransient<WorkoutPage>();
            builder.Services.AddTransient<IShowValidatorDisplay, ShowValidatorDisplay>();
            builder.Services.AddTransient<ExerciseValidator>();
            builder.Services.AddTransient<WorkoutValidator>();
            builder.Services.AddTransient<WorkoutSetValidator>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
