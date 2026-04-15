using DocumentImporter.Domain;

namespace DocumentImporter.Infrastructure.Repositories;

public interface IDocumentRepository
{
    void SaveDocuments(List<Document> documents, List<DocumentItem> items);
    List<Document> GetAll();
    Document? GetById(int id);
    bool HasData();
    void ClearAll();
}