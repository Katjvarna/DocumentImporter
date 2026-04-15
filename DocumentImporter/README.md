# DocumentImporter
 
Desktopowa aplikacja WPF zbudowana na platformie .NET 8, umo¿liwiaj¹ca import, przechowywanie i przegl¹danie dokumentów z plików CSV z wykorzystaniem bazy danych SQLite.
 
---

## Funkcjonalnoœci
 
- Import danych z plików CSV (`Documents.csv`, `DocumentItems.csv`) do lokalnej bazy danych SQLite
- Przegl¹danie dokumentów w formie tabelarycznej z mo¿liwoœci¹ sortowania
- Filtrowanie dokumentów po imieniu, nazwisku, mieœcie lub typie dokumentu
- Podgl¹d szczegó³ów dokumentu wraz z jego pozycjami (produkty, ceny, VAT)
- Wyliczanie wartoœci netto, VAT i brutto dla ka¿dego dokumentu
- Trwa³e przechowywanie danych — dane pozostaj¹ dostêpne miêdzy uruchomieniami aplikacji
- Mo¿liwoœæ wyczyszczenia widoku tabeli bez usuwania danych z bazy

---

## U¿yte technologie
 
| Warstwa | Technologia |
|---|---|
| Interfejs u¿ytkownika | WPF (.NET 8) |
| ORM | Entity Framework Core 8 |
| Baza danych | SQLite |
| Parsowanie CSV | CsvHelper |
| Wzorzec architektoniczny | MVVM |
 
---

## Jak u¿ywaæ aplikacji
 
1. **Uruchom aplikacjê** — na starcie wyœwietla siê puste okno z tabel¹
2. **Klikniêcie "Import CSV oraz zapis do bazy danych"** — wczytuje dane z plików CSV z folderu `Assets` i zapisuje je do bazy danych
3. **Klikniêcie "Za³aduj z bazy"** — pobiera dane z bazy danych i wyœwietla je w tabeli
4. **Pole wyszukiwania** — filtruje dokumenty po imieniu, nazwisku lub mieœcie
5. **Lista Rozwijana "Typ"** — filtruje po typie dokumentu (Invoice, Order, Receipt)
6. **KIlikniêcie dwukrotnie Wiersza** — otwiera okno szczegó³ów dokumentu z list¹ jego pozycji
7. **Klikniêcie "Wyczyœæ"** — czyœci widok tabeli (dane pozostaj¹ w bazie danych)
---

## Paczki NuGet
 
- `Microsoft.EntityFrameworkCore` 8.0.25
- `Microsoft.EntityFrameworkCore.Sqlite` 8.0.25
- `Microsoft.EntityFrameworkCore.Tools` 8.0.25
- `CsvHelper` 33.1.0
- `CommunityToolkit.Mvvm` 8.4.2