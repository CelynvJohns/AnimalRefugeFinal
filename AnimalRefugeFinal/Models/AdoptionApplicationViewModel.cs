using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    namespace AnimalRefugeFinal.Models
    {
        public class AdoptionApplicationViewModel
        {
            // Properties for adoption application form fields
            public int PetId { get; set; }

            [Required(ErrorMessage = "Please provide reasons for adoption.")]
            public string Reasons { get; set; }

            // Add other necessary properties for your adoption application form

            // Properties to pass pet information to the view
            public string PetName { get; set; }
            // Add other necessary properties for displaying pet information

            // Constructor to initialize the view model with pet information
            public AdoptionApplicationViewModel(Pet pet)
            {
                PetId = pet.Id;
                PetName = pet.Name;
                // Initialize other properties for displaying pet information
            }

            // Default constructor for model binding
            public AdoptionApplicationViewModel()
            {
            }
        }
    }

}
