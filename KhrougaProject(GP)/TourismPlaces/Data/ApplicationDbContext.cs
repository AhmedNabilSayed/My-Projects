using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TourismPlaces.Data.Models;

namespace TourismPlaces.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Government> Governments { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<Photo> photos { get; set; }


    }
}
