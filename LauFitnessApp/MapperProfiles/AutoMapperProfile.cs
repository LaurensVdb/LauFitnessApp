using AutoMapper;
using DataAcces.Entities;
using LauFitnessApp.Models;

namespace LauFitnessApp.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Exercise, ExerciseDTO>();
            CreateMap<ExerciseDTO, Exercise>();
            CreateMap<WorkoutDTO, Workout>()
                  .ForMember(x => x.WorkoutSets, opt => opt.Ignore());
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<WorkoutSetDTO, WorkoutSet>();
            CreateMap<WorkoutSet, WorkoutSetDTO>();
        }
    }
}
