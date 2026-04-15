using DocumentImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentImporter.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _context;

    public DocumentRepository(AppDbContext context)
    {
        _context = context;
    }

    // Sprawdza czy w bazie są już dane
    public bool HasData() => _context.Documents.Any();

    public void SaveDocuments(List<Document> documents, List<DocumentItem> items)
    {
        _context.Documents.AddRange(documents);
        _context.SaveChanges();

        // Najpierw zapisujemy dokumenty, potem items
        _context.DocumentItems.AddRange(items);
        _context.SaveChanges();
    }

    public List<Document> GetAll()
    {
        return _context.Documents
            .Include(d => d.Items)  // dołącz pozycje do każdego dokumentu
            .OrderBy(d => d.Id)
            .ToList();
    }

    public Document? GetById(int id)
    {
        return _context.Documents
            .Include(d => d.Items)
            .FirstOrDefault(d => d.Id == id);
    }
    public void ClearAll()
    {
        _context.DocumentItems.RemoveRange(_context.DocumentItems);
        _context.Documents.RemoveRange(_context.Documents);
        _context.SaveChanges();
    }
}