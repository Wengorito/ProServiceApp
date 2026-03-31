# Projekt API (.NET 8.0)

Projekt został zrealizowany w technologii **.NET 8.0** w architekturze warstwowej z podziałem na:

- **Presentation**
- **Application**
- **Domain**

Celem takiego podejścia jest zwiększenie czytelności, skalowalności oraz testowalności aplikacji.

---

## Architektura aplikacji

### 1. Warstwa prezentacji (Presentation)

Warstwa prezentacji odpowiada za komunikację z klientem poprzez HTTP.

Główne założenia:

- Kontrolery obsługują żądania HTTP oraz mapowanie danych wejściowych i wyjściowych (**Model Binding**).
- Komunikacja z warstwą aplikacji odbywa się poprzez obiekty **DTO**, zawierające wyłącznie niezbędne dane.
  - Wykluczenie z obiegu całych modeli domenowych poprawia **bezpieczeństwo**, **wydajność** oraz utrzymuje separację zależności od warstw niższych.
- DTO zostały wstępnie podzielone na:
  - `Requests`
  - `Responses`  
  co zwiększa czytelność i wspiera **Separation of Concerns**.
- W obiektach typu `Request DTO` zastosowano **DataAnnotations** (`[Required]`, `[Range]` itd.), aby walidować formalną poprawność danych już na etapie mapowania.
  - Dzięki temu kontrolery pozostają „thin” (**DRY**, brak powielania walidacji).

#### Możliwe dalsze ulepszenia

- Zastosowanie **Minimal API**.
- Podział DTO na **Commands / Queries** w przypadku rozwoju aplikacji (CQRS).

---

### 2. Warstwa aplikacji (Application)

Warstwa aplikacji odpowiada za realizację logiki biznesowej i obsługę przypadków użycia.

Główne elementy:

- **Serwisy**:
  - wstrzykiwane do kontrolerów,
  - realizują logikę żądań,
  - mapują dane DTO ↔ modele,
  - komunikują się z warstwą domenową,
  - wykonują sprawdzanie poprawności pobranych danych (np. `null-checking`).
- **Walidatory**:
  - wymuszają zgodność danych z wymaganiami biznesowymi,
  - zwracają czytelne błędy walidacji,
  - pozwalają trzymać logikę walidacyjną w jednym miejscu.

Taki podział zwiększa skalowalność, czytelność i testowalność kodu.

#### Możliwe dalsze ulepszenia

- Zastosowanie **custom errors** zamiast generycznych wyjątków.
- Wprowadzenie **Result Pattern**.
- Zastosowanie **FluentValidation** zamiast rzucania pojedynczych wyjątków.

---

### 3. Warstwa domenowa (Domain)

Najniższa warstwa odpowiedzialna za operacje odczytu i zapisu danych.

Założenia:

- Warstwa domenowa realizuje operacje na bazie danych.
- Przyjmuje i zwraca **modele domenowe**, odpowiadające encjom w bazie.
- Modele wymuszają zgodność typu danych ze schematem bazy (np. atrybuty).
- Zastosowano dziedziczenie (`TaskBase`), co:
  - redukuje ilość kodu,
  - centralizuje podstawowe właściwości,
  - umożliwia wykonywanie wspólnych operacji na obiektach dziedziczących.
- Zastosowano wzorzec **Repository Pattern**:
  - repozytorium wystawia metody do typowych operacji (pobieranie, filtracja, sortowanie),
  - zwiększa reużywalność kodu,
  - poprawia wydajność (operacje wykonywane po stronie bazy są szybsze i zmniejszają transfer danych).
- Repozytorium zostało oparte o interfejs i wstrzykiwane jako **singleton**:
  - umożliwia podmianę implementacji (ORM, stored procedures itd.),
  - wspiera mockowanie w testach,
  - w kontekście tego zadania wystarczyłaby również prosta klasa statyczna.

#### Możliwe dalsze ulepszenia

- Dodanie wzorca **UnitOfWork**.
- Podział repozytoriów na encje:
  - `TaskRepository`
  - `EmployeeRepository`

---

## Testy API

### Testy jednostkowe (Unit Tests)

Projekt zawiera testy jednostkowe obejmujące:

- pokrycie każdej możliwej ścieżki egzekucji programu (**100% coverage**),
- testy przypadków brzegowych (**edge cases**),
- mockowanie zależności zewnętrznych (**Moq**) tak, aby testy:
  - nie odwoływały się do bazy danych,
  - nie korzystały z warstwy HTTP.

#### Możliwe dalsze ulepszenia

Kolejnym krokiem automatyzacji QA może być dodanie:

- **testów integracyjnych (integration tests)**.
