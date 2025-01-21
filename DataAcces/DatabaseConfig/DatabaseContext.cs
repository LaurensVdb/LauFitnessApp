using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.DatabaseConfig
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutSet> WorkoutSets { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {


        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
            base.OnConfiguring(optionsBuilder);
        }

       
    }
}
