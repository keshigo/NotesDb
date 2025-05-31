using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleProject.NET.Data.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
        
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(n => n.Description)
            .IsRequired();
            
        builder.HasOne(n => n.User)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.UserId);
    }
}