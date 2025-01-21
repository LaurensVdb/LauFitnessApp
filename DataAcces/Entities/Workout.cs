using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace DataAcces.Entities
{
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string WorkoutName { get; set; } = "";

        public  Collection<WorkoutSet> WorkoutSets { get; set; }
    }
}
