using DocumentImporter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DocumentImporter;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Tworzy plik app.db jeśli nie istnieje
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
    }
}