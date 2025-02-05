using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(0, 100)]
        public int Proficiency { get; set; }

        public string Category { get; set; }

        public int YearsOfExperience { get; set; }
    }
}
