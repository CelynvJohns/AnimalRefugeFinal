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

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            using (var scoped = serviceProvider.CreateScope())
            {
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
}
