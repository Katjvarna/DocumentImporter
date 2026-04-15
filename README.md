# DocumentImporter
 
Desktopowa aplikacja WPF zbudowana na platformie .NET 8, umożliwiająca import, przechowywanie i przeglądanie dokumentów z plików CSV z wykorzystaniem bazy danych SQLite.
 
---

## Funkcjonalności
 
- Import danych z plików CSV (`Documents.csv`, `DocumentItems.csv`) do lokalnej bazy danych SQLite
- Przeglądanie dokumentów w formie tabelarycznej z możliwością sortowania
- Filtrowanie dokumentów po imieniu, nazwisku, mieście lub typie dokumentu
- Podgląd szczegółów dokumentu wraz z jego pozycjami (produkty, ceny, VAT)
- Wyliczanie wartości netto, VAT i brutto dla każdego dokumentu
- Trwałe przechowywanie danych — dane pozostają dostępne między uruchomieniami aplikacji
- Możliwość wyczyszczenia widoku tabeli bez usuwania danych z bazy

---

## Użyte technologie
 
| Warstwa | Technologia |
|---|---|
| Interfejs użytkownika | WPF (.NET 8) |
| ORM | Entity Framework Core 8 |
| Baza danych | SQLite |
| Parsowanie CSV | CsvHelper |
| Wzorzec architektoniczny | MVVM |
 
---

## Jak używać aplikacji
 
1. **Uruchom aplikację** — na starcie wyświetla się puste okno z tabelą
2. **Kliknięcie "Import CSV oraz zapis do bazy danych"** — wczytuje dane z plików CSV z folderu `Assets` i zapisuje je do bazy danych
3. **Kliknięcie "Załaduj z bazy"** — pobiera dane z bazy danych i wyświetla je w tabeli
4. **Pole wyszukiwania** — filtruje dokumenty po imieniu, nazwisku lub mieście
5. **Lista Rozwijana "Typ"** — filtruje po typie dokumentu (Invoice, Order, Receipt)
6. **KIliknięcie dwukrotnie Wiersza** — otwiera okno szczegółów dokumentu z listą jego pozycji
7. **Kliknięcie "Wyczyść"** — czyści widok tabeli (dane pozostają w bazie danych)
---

## Paczki NuGet
 
- `Microsoft.EntityFrameworkCore` 8.0.25
- `Microsoft.EntityFrameworkCore.Sqlite` 8.0.25
- `Microsoft.EntityFrameworkCore.Tools` 8.0.25
- `CsvHelper` 33.1.0
- `CommunityToolkit.Mvvm` 8.4.2
