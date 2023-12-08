using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnimalRefugeFinal.Models
{
    public class User : IdentityUser
    {

        // Navigation property for favorites
        public ICollection<Favorite> Favorites { get; set; }

        // Navigation property for adoption applications
        public ICollection<AdoptionApplication> AdoptionApplications { get; set; }

    }
}
