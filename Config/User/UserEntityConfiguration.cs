using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.UserName).HasMaxLength(128);
        builder.Property(user => user.Password).HasMaxLength(256);

        builder.HasMany(user => user.Notes)
               .WithOne(note => note.User)
               .HasForeignKey(note => note.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
