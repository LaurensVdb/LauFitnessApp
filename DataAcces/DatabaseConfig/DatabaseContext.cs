using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.DatabaseConfig
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutSet> WorkoutSets { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        /*
        public DatabaseContext()
        {

        }
        */
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WorkoutSet>()
           .HasOne(e => e.Exercise)
           .WithMany(u => u.WorkoutSets)
           .OnDelete(DeleteBehavior.Restrict);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
            base.OnConfiguring(optionsBuilder);
        }


    }
}
