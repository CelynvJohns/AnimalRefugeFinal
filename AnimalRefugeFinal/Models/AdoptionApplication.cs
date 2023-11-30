using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number greater than 0.")]
        public int NumberOfPets { get; set; }

        public Pet[] Pets { get; set; }

        public CurrentPet[] CurrentPet { get; set; } // Corrected property name to CurrentPets
        public string CurrentPetName { get; set; }
        public int CurrentPetAge { get; set; }
        public int CurrentPetBreed { get; set; }
        public string VetName { get; set; }
        public int VetPhoneNumber { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; } // "Pending", "Approved", "Rejected"

        public CurrentHumans[] CurrentHumans { get; set; } // Corrected property name to Humans
        public string CurrentHumanFirstName { get; set; }
        public string CurrentHumanLastName { get; set; }
        public string CurrentHumanAge { get; set; }

        public int CurrentPetNumber { get; set; }
        public int CurrentHumansNumber { get; set; }

        public bool WantsToAdoptDog { get; set; }

        public bool HasFencedInYard { get; set; }
    }

    public class CurrentHumans
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; } 
    }

    public class CurrentPet
    {
        [Required]
        public string CurrentPetName { get; set; }

        [Required]
        public int CurrentPetAge { get; set; }

        [Required]
        public int CurrentPetBreed { get; set; }

        [Required]
        public string VetName { get; set; }

        [Required]
        public int VetPhoneNumber { get; set; }


    }
}
