using ConsoleProject.NET.Configurations.Database;
using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConsoleProject.NET.Data;

public class AppDbContext : DbContext
{
    private readonly ApplicationDbContextSettings _dbContextSettings;

    public DbSet<User> Users => Set<User>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<JwtToken> JwtTokens { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<ApplicationDbContextSettings> dbContextSettings)
        : base(options)
    {
        _dbContextSettings = dbContextSettings.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_dbContextSettings.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        modelBuilder.Entity<JwtToken>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired().ValueGeneratedNever();
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<JwtToken>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
