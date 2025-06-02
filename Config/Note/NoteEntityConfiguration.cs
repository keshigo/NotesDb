using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NoteEntityConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(note => note.Id);

        builder.Property(note => note.Title).HasMaxLength(256);

        builder.HasOne(note => note.User)
               .WithMany(user => user.Notes)
               .HasForeignKey(note => note.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
