using DocumentImporter.Domain;

namespace DocumentImporter.ViewModels;

public class DocumentDetailViewModel : BaseViewModel
{
    public Document Document { get; }

    // Wyliczamy wartości podsumowania na podstawie pozycji
    public decimal TotalNet => Document.Items.Sum(i => i.Price * i.Quantity);
    public decimal TotalTax => Document.Items.Sum(i => i.Price * i.Quantity * i.TaxRate / 100);
    public decimal TotalGross => TotalNet + TotalTax;
    public int ItemsCount => Document.Items.Count;

    public DocumentDetailViewModel(Document document)
    {
        Document = document;
    }
}