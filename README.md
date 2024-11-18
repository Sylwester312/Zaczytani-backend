# Dokumentacja Techniczna Aplikacji

## Spis Treści
1. [Opis projektu](#opis-projektu)
2. [Struktura projektu](#struktura-projektu)
3. [Konfiguracja środowiska](#konfiguracja-środowiska)
4. [Konfiguracja bazy danych](#konfiguracja-bazy-danych)
5. [Instrukcja uruchomienia](#instrukcja-uruchomienia)
6. [Struktura warstw](#struktura-warstw)
   - [Domain](#domain)
   - [Application](#application)
   - [Infrastructure](#infrastructure)
   - [API](#api)
7. [Dodatkowe informacje](#dodatkowe-informacje)

---

## Opis projektu
Projekt jest aplikacją wielowarstwową zbudowaną w technologii .NET, która korzysta z podejścia **Clean Architecture**. Aplikacja działa w środowisku .NET Core i wykorzystuje bazę danych **MSSQL**. Architektura projektu została podzielona na 4 warstwy: **Domain**, **Infrastructure**, **Application**, oraz **API**. Każda z warstw pełni inną funkcję i jest odpowiedzialna za różne aspekty działania aplikacji, co pozwala na lepsze zarządzanie kodem i jego skalowalność.

## Struktura projektu
```
Projekt 
├── Zaczytani.API 
├── Zaczytani.Application 
├── Zaczytani.Domain 
└── Zaczytani.Infrastructure
```

### Opis warstw
- **Domain** - zawiera definicje encji i logikę biznesową.
- **Application** - przechowuje logikę aplikacji, w tym interfejsy, komendy, zapytania oraz usługi.
- **Infrastructure** - warstwa odpowiedzialna za dostęp do danych oraz komunikację z bazą danych MSSQL.
- **API** - warstwa prezentacji, która udostępnia API i umożliwia komunikację z aplikacją zewnętrzną.

## Konfiguracja środowiska

### Wymagania systemowe:
- .NET SDK (wersja zgodna z wersją używaną w projekcie, np. .NET 8.0 lub nowszy).
- SQL Server (np. SQL Server 2019 lub nowszy).
- Visual Studio.

### Klonowanie repozytorium:

Skopiuj repozytorium projektu:
```bash
git clone https://devtools.wi.pb.edu.pl/bitbucket/scm/pt2024zacz/zaczytani-backend.git cd zaczytani-backend
```
## Konfiguracja bazy danych

### Konfiguracja połączenia:
W pliku `appsettings.Development.json` w warstwie **API** znajdź sekcję `ConnectionStrings` i zaktualizuj ciąg połączenia:

```json
"ConnectionStrings": {
  "ZaczytaniDb": "Server=(localdb)\\mssqllocaldb;Database=Zaczytani;Trusted_Connection=True"
}
```
### Wykonanie migracji i aktualizacja bazy danych:
Aby zaktualizować bazę danych do najnowszej wersji zgodnie z migracjami, otwórz **Narzędzia → Menadżer pakietów NuGet → Konsola Menedżera Pakietów** w Visual Studio lub terminal i wykonaj komendę `Update-Database`. Ten krok wykona migracje i zaktualizuje strukturę bazy danych.

### Zweryfikowanie poprawności migracji:
1. Otwórz **SQL Server Management Studio** i połącz się z instancją SQL Server.
2. Zweryfikuj poprawność utworzenia bazy **Zaczytani** oraz poprawność utworzenia tabel.

## Instrukcja uruchomienia

### Uruchomienie aplikacji:
1. Otwórz projekt w Visual Studio lub Visual Studio Code.
2. Ustaw projekt startowy na **API**.
3. Uruchom aplikację, korzystając ze skrótu F5 lub komendy `dotnet run --project API`.  
Aplikacja będzie dostępna pod adresem `http://localhost:5062` lub `https://localhost:7083`.

### Testowanie API:
Po uruchomieniu możesz przetestować API za pomocą narzędzi takich jak **Postman** lub **Swagger**. Swagger powinien być dostępny pod adresem `https://localhost:7083/swagger`.

## Struktura warstw

Poniżej znajduje się szczegółowy opis poszczególnych warstw projektu:

### Domain
- Warstwa **Domain** zawiera modele domenowe, czyli główne encje, oraz logikę biznesową.
- Tutaj definiowane są interfejsy, które następnie są implementowane w warstwie **Infrastructure**.
- Encje reprezentują tabele w bazie danych, a logika biznesowa jest izolowana od logiki aplikacyjnej.

### Application
- Warstwa **Application** pełni rolę „mózgu” aplikacji, gdzie zarządzane są operacje (komendy) oraz zapytania (queries).
- Definiowane są tutaj interfejsy, które łączą warstwę **Domain** z warstwą **Infrastructure**.
- Zawiera także usługi oraz wzorce **CQRS** (Command Query Responsibility Segregation).

### Infrastructure
- **Infrastructure** to warstwa, która implementuje wszystkie usługi potrzebne do komunikacji z zewnętrznymi źródłami, takimi jak baza danych.
- Ta warstwa zawiera szczegóły konfiguracji **Entity Framework Core** oraz migracje.
- To właśnie tutaj realizowane są implementacje interfejsów zdefiniowanych w warstwie **Domain** i **Application**.

### API
- Warstwa **API** to punkt wejściowy aplikacji, który komunikuje się z innymi warstwami i obsługuje żądania HTTP.
- Zawiera kontrolery, które przyjmują zapytania HTTP, a następnie kierują je do odpowiednich usług w warstwie **Application**.
- **API** także definiuje konfigurację **Swaggera**, aby umożliwić interaktywne testowanie API.

## Dodatkowe informacje
### Tworzenie migracji

Jeśli chcesz dodać nową migrację, wykonaj poniższe kroki:

1. Otwórz `Narzędzia → Menadżer pakietów NuGet → Konsola Menedżera Pakietów`
2. Upewnij się, że warstwa **Infrastructure** jest ustawiona jako projekt migracji.
3. W konsoli wykonaj: `Add-Migration <NazwaMigracji>`
4. Po utworzeniu migracji zaktualizuj bazę danych: `Update-Database`
