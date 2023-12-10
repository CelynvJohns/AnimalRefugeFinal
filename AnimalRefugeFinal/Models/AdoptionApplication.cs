using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Please enter your first name ")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name ")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your home address")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number greater than 0.")]
        public int NumberOfPets { get; set; }

        public List<Pet> Pets { get; set; }

        


        public List<CurrentPet> CurrentPet { get; set; } // Corrected property name to CurrentPets
        

        [Required]
        public string Reason { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; } // "Pending", "Approved", "Rejected"

        public List<CurrentHumans> CurrentHumans { get; set; } // Corrected property name to Humans

        public int CurrentPetNumber { get; set; }
        public int CurrentHumansNumber { get; set; }

        public bool WantsToAdoptDog { get; set; }

        public bool HasFencedInYard { get; set; }
        public int PetId { get; internal set; }
        public string Reasons { get; internal set; }
    }

    public class CurrentHumans
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter age")]
        [Range(0,130)]
        public int Age { get; set; } 
    }

    public class CurrentPet
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "Please enter current pet name")]
        [StringLength(50)]
        public string CurrentPetName { get; set; }

        [Required(ErrorMessage = "Please enter current pet age")]
        [Range(0, 40)]
        public int CurrentPetAge { get; set; }

        [Required(ErrorMessage = "Please enter current pet breed")]
        [StringLength(50)]
        public string CurrentPetBreed { get; set; }

        [Required(ErrorMessage = "Please enter correct vet name")]
        [StringLength(50)]
        public string VetName { get; set; }

        [Required(ErrorMessage = "Please enter correct vet phone number")]
        [StringLength(11)]
        public string VetPhoneNumber { get; set; }


    }
}
