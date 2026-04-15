using CsvHelper.Configuration;
using DocumentImporter.Domain;

namespace DocumentImporter.Infrastructure.CsvImport
{
    public class DocumentItemMap : ClassMap<DocumentItem>
    {
        public DocumentItemMap()
        {
            Map(m => m.DocumentId).Name("DocumentId");
            Map(m => m.Ordinal).Name("Ordinal");
            Map(m => m.Product).Name("Product");
            Map(m => m.Quantity).Name("Quantity");
            Map(m => m.Price).Name("Price");
            Map(m => m.TaxRate).Name("TaxRate");
            Map(m => m.Id).Ignore();
            Map(m => m.Document).Ignore();
        }
    }
}
