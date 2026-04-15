using DocumentImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentImporter.Infrastructure;

public class AppDbContext : DbContext
{
    // Te dwie właściwości = dwie tabele w bazie
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentItem> DocumentItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Baza SQLite — plik app.db powstanie automatycznie
        optionsBuilder.UseSqlite("Data Source=app.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>()
            .HasMany(d => d.Items)
            .WithOne(i => i.Document)
            .HasForeignKey(i => i.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}