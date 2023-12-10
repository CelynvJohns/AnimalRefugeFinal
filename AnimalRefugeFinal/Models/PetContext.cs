using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimalRefugeFinal.Models
{
    public class PetContext : IdentityDbContext<User>
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
                new Pet { Id = 1, Name = "Fluffy", Species = "Cat", Age = 2, Description = "A cute fluffy cat.", BondedBuddyStatus = "None", SpecialCareInstructions = "Needs exercise" },
                new Pet { Id = 2, Name = "Buddy", Species = "Dog", Age = 3, Description = "A friendly dog looking for a home.", BondedBuddyStatus = "None", SpecialCareInstructions = "Is diabetic"},
                new Pet { Id = 3, Name = "Moo", Species = "Cat", Age = 1, Description = "Bonded Buddies with Colby. An affectionate cat who meows a lot.", BondedBuddyStatus = "Colby", SpecialCareInstructions = "Has to go with Colby. Needs a good home." },
                new Pet { Id = 4, Name = "Colby", Species = "Cat", Age = 5, Description = "Bonded Buddies with Moo. Very skitish, but loves to be pet.", BondedBuddyStatus = "Moo", SpecialCareInstructions = "Has to go with Moo. Needs a good home." },
                new Pet { Id = 5, Name = "Juno", Species = "Bunny", Age = 10, Description = "Elderly bunny who needs a new home. Loves carrots.", BondedBuddyStatus = "None", SpecialCareInstructions = "Needs a specialized diet to maintain health." },
                new Pet { Id = 6, Name = "Peaches", Species = "Dog", Age = 6, Description = "An affectionate dog who loves to play.", BondedBuddyStatus = "None", SpecialCareInstructions = "Requires 10 hours of play a week." },
                new Pet { Id = 7, Name = "Olo", Species = "Dog", Age = 4, Description = "A overly affectionate dog who needs a new home.", BondedBuddyStatus = "None", SpecialCareInstructions = "Not good with kids." },
                new Pet { Id = 8, Name = "Daisey", Species = "Reptile", Age = 19, Description = "A turtle who loves to swim and stare at you.", BondedBuddyStatus = "None", SpecialCareInstructions = "Great with kids, needs to be with people or gets too lonely." }
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

            //configure relationship for favorite and User
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<AdoptionApplication>()
            .HasMany(a => a.CurrentPet)
            .WithOne()
            .HasForeignKey(a => a.Id);

            modelBuilder.Entity<AdoptionApplication>()
                .HasMany(a => a.CurrentHumans)
                .WithOne()
                .HasForeignKey(a => a.Id);

            // Configure relationship for AdoptionApplications
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(a => a.User)
                .WithMany(u => u.AdoptionApplications)
                .HasForeignKey(a => a.UserId);

            // Ensure that Identity entities are not redefined here
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();

        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            using var scoped = serviceProvider.CreateScope();
            UserManager<User> userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "petadmin";
            string pwd = "adminpet";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User() { UserName = username };
                var result = await userManager.CreateAsync(user, pwd);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
