using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [Required]
        public string Reasons { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string Status { get; set; } // "Pending", "Approved", "Rejected"
    }
}
