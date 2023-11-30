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

        public string CurrentPets { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; } // "Pending", "Approved", "Rejected"
    }
}
