using System.ComponentModel.DataAnnotations;

namespace AnimalRefugeFinal.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // I think adding in a first and last for the profile would be nice, so I added it but commented it out
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        public string Role { get; set; } // "Admin" or "User"

        // Navigation property for favorites
        public ICollection<Favorite> Favorites { get; set; }

        // Navigation property for adoption applications
        public ICollection<AdoptionApplication> AdoptionApplications { get; set; }
    }
}
