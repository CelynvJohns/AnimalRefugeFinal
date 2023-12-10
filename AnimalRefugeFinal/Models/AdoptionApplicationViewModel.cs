using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace AnimalRefugeFinal.Models
{
    namespace AnimalRefugeFinal.Models
    {
        public class AdoptionApplicationViewModel
        {
            // Properties for adoption application form fields
            public int PetId { get; set; }

            [Required(ErrorMessage = "Please provide reasons for adoption.")]
            [StringLength(200)]
            public string Reasons { get; set; }

            [Required]
            [Range(0, 50)]
            public int PetAge { get; set; }

            [Required]
            [StringLength(30)]
            public string PetSpecies { get; set; }

            [Required]
            [StringLength (200)]
            public string PetDescription { get; set; }

            // Add other necessary properties for adoption application form

            // Properties to pass pet information to the view
            public string PetName { get; set; }
            // Add other necessary properties for pet information

            // Constructor to initialize the view model with pet information
            public AdoptionApplicationViewModel(Pet pet)
            {
                PetId = pet.Id;
                PetName = pet.Name;
                PetAge = pet.Age;
                PetSpecies = pet.Species;
                PetDescription = pet.Description;

                // Initialize other properties for pet information
            }

            // Default constructor for model binding
            public AdoptionApplicationViewModel()
            {
            }
        }
    }

}
