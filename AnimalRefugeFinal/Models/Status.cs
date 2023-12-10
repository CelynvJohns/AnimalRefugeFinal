using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } // Examples: "Pending", "Approved", "Rejected"

        // Navigation property for adoption applications
        public ICollection<AdoptionApplication>? AdoptionApplications { get; set; }
    }
}
