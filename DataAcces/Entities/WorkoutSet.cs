using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Entities
{
    public class WorkoutSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public  Exercise Exercise { get; set; }
        public  Workout Workout { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }

    }
}
