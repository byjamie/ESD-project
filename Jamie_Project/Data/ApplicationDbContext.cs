using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Jamie_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Jamie_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
