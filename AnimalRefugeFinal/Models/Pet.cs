using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        public int Age { get; set; }

        public string BondedBuddyStatus { get; set; }

        [Required]
        public string Description { get; set; }

        public string SpecialCareInstructions { get; set; }

        // Navigation property for favorites
        public ICollection<Favorite> Favorites { get; set; }

        public string Reason { get; set; }

        public bool IsAdopted { get; set; }

    }
}
