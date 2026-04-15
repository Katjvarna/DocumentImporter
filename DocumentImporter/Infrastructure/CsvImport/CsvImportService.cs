using CsvHelper;
using CsvHelper.Configuration;
using DocumentImporter.Domain;
using System.Globalization;
using System.IO;

namespace DocumentImporter.Infrastructure.CsvImport
{

    public class CsvImportService : ICsvImportService
    {
        private readonly CultureInfo _plCulture = new CultureInfo("pl-PL");

        public List<Document> ImportDocuments(string filePath)
        {
            var config = new CsvConfiguration(_plCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            csv.Context.RegisterClassMap<DocumentItemMap>();
            return csv.GetRecords<Document>().ToList();
        }

        public List<DocumentItem> ImportDocumentItems(string filePath)
        {
            var config = new CsvConfiguration(_plCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            csv.Context.RegisterClassMap<DocumentItemMap>();
            return csv.GetRecords<DocumentItem>().ToList();
        }
    }
}
