using DocumentImporter.Domain;

namespace DocumentImporter.Infrastructure.CsvImport;

public interface ICsvImportService
{
    List<Document> ImportDocuments(string filePath);
    List<DocumentItem> ImportDocumentItems(string filePath);
}