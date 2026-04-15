using System.IO;
using DocumentImporter.Domain;
using DocumentImporter.Infrastructure;
using DocumentImporter.Infrastructure.CsvImport;
using DocumentImporter.Infrastructure.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DocumentImporter.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IDocumentRepository _repository;
    private readonly ICsvImportService _csvImportService;

    // ObservableCollection automatycznie odświeża UI gdy dodajemy/usuwamy elementy
    public ObservableCollection<Document> Documents { get; } = new();

    // Właściwość filtrowania
    private string _filterText = string.Empty;
    public string FilterText
    {
        get => _filterText;
        set
        {
            SetProperty(ref _filterText, value);
            ApplyFilter();
        }
    }

    private string _filterType = "Wszystkie";
    public string FilterType
    {
        get => _filterType;
        set
        {
            SetProperty(ref _filterType, value);
            ApplyFilter();
        }
    }

    // Lista typów do ComboBoxa
    public List<string> DocumentTypes { get; } = new()
    {
        "Wszystkie", "Invoice", "Order", "Receipt"
    };

    private Document? _selectedDocument;
    public Document? SelectedDocument
    {
        get => _selectedDocument;
        set => SetProperty(ref _selectedDocument, value);
    }

    private string _statusMessage = "Gotowy.";
    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public ICommand ImportCommand { get; }
    public ICommand ShowDetailCommand { get; }
    public ICommand LoadFromDatabaseCommand { get; }
    public ICommand ClearCommand { get; }

    // Przechowuje wszystkie dokumenty (przed filtrowaniem)
    private List<Document> _allDocuments = new();

    public MainViewModel()
    {
        _repository = new DocumentRepository(new AppDbContext());
        _csvImportService = new CsvImportService();

        ImportCommand = new RelayCommand(ImportData);
        ShowDetailCommand = new RelayCommand(ShowDetail, () => SelectedDocument != null);
        LoadFromDatabaseCommand = new RelayCommand(LoadFromDatabase);
        ClearCommand = new RelayCommand(ClearGrid);

        // Usuwamy automatyczne LoadFromDatabase()
        StatusMessage = "Wybierz akcję: załaduj dane z bazy lub zaimportuj CSV.";
    }

    private void LoadFromDatabase()
    {
        _allDocuments = _repository.GetAll();
        RefreshDocuments(_allDocuments);
        StatusMessage = $"Załadowano {_allDocuments.Count} dokumentów z bazy.";
    }

    private void ImportData()
    {
        try
        {
            if (_repository.HasData())
            {
                var result = MessageBox.Show(
                    "Baza zawiera już dane. Czy chcesz zaimportować ponownie?",
                    "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No) return;

                _repository.ClearAll();
            }

            // Ścieżki do plików CSV w folderze Assets
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var documentsPath = Path.Combine(baseDir, "Assets", "Documents.csv");
            var itemsPath = Path.Combine(baseDir, "Assets", "DocumentItems.csv");

            if (!File.Exists(documentsPath) || !File.Exists(itemsPath))
            {
                MessageBox.Show("Nie znaleziono plików CSV w folderze Assets!",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StatusMessage = "Importowanie...";

            var documents = _csvImportService.ImportDocuments(documentsPath);
            var items = _csvImportService.ImportDocumentItems(itemsPath);

            _repository.SaveDocuments(documents, items);
            LoadFromDatabase();

            StatusMessage = $"Import zakończony! Zaimportowano {documents.Count} dokumentów.";
            MessageBox.Show($"Zaimportowano {documents.Count} dokumentów i {items.Count} pozycji.",
                "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            StatusMessage = "Błąd importu!";
            MessageBox.Show($"Błąd podczas importu:\n{ex.Message}",
                "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ShowDetail()
    {
        if (SelectedDocument == null) return;

        var detailVm = new DocumentDetailViewModel(SelectedDocument);
        var detailWindow = new DocumentImporter.Views.DocumentDetailWindow(detailVm);
        detailWindow.ShowDialog();
    }

    private void ApplyFilter()
    {
        var filtered = _allDocuments.AsEnumerable();

        // Filtruj po tekście (imię, nazwisko, miasto)
        if (!string.IsNullOrWhiteSpace(FilterText))
        {
            filtered = filtered.Where(d =>
                d.FirstName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                d.LastName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                d.City.Contains(FilterText, StringComparison.OrdinalIgnoreCase));
        }

        // Filtruj po typie dokumentu
        if (FilterType != "Wszystkie")
        {
            filtered = filtered.Where(d => d.Type == FilterType);
        }

        RefreshDocuments(filtered.ToList());
    }

    private void RefreshDocuments(List<Document> docs)
    {
        Documents.Clear();
        foreach (var doc in docs)
            Documents.Add(doc);
    }
    private void ClearGrid()
    {
        _allDocuments.Clear();
        Documents.Clear();
        SelectedDocument = null;
        StatusMessage = "Wyczyszczono tabelę.";
    }

}