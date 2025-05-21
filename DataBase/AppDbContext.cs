using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_NoteThirdTask.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(z => z.Notes)
                .WithOne(o => o.User)
                .HasForeignKey(v => v.UserId);
        }
    }
}