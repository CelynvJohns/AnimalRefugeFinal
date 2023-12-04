using Microsoft.EntityFrameworkCore;

namespace AnimalRefugeFinal.Models
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Status> Statuses { get; set; } // Add Status DbSet
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; } // Add AdoptionApplication DB set
        public DbSet<User> Users { get; set; } // add User info to database
        public DbSet<Favorite> Favorites { get; set; } // add User favorite to database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Custom configurations for the Pet entity go here

            modelBuilder.Entity<Pet>().HasData(
                new Pet { Id = 1, Name = "Fluffy", Species = "Cat", Age = 2, Description = "A cute fluffy cat.", BondedBuddyStatus = "None", SpecialCareInstructions = "Handle with care" },
                new Pet { Id = 2, Name = "Buddy", Species = "Dog", Age = 3, Description = "A friendly dog looking for a home.", BondedBuddyStatus = "None", SpecialCareInstructions = "Loves walks and playtime" }
                );

            //Other configurations or seed data go here
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Approved" },
                new Status { Id = 3, Name = "Rejected" }
            );

            // Add the configuration for the relationship between AdoptionApplication and Status
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(a => a.Status)
                .WithMany()
                .HasForeignKey(a => a.StatusId);

        }
    }
}
