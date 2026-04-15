using DocumentImporter.ViewModels;
using System.Windows;

namespace DocumentImporter.Views;

public partial class DocumentDetailWindow : Window
{
    public DocumentDetailWindow(DocumentDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}