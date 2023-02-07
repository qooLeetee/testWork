using Microsoft.EntityFrameworkCore;
using testWork.Models;

namespace testWork
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Database=test;Username=postgres;Password=postgres"
            );
        }
    }
}