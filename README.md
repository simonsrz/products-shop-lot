# Zadanie rekrutacyjne LOT
## Jak odpalić projekt
- `git clone https://github.com/simonsrz/products-shop-lot.git`
- Przejdź do folderu z projektem w terminali
- `dotnet restore` żeby zainstalować wszystkie użyte pakiety
- Zaktualizować datasource `appsettings.json` do swojej bazy SQL Server
- `dotnet tool install --global dotnet-ef`
- `dotnet ef database update`
- `dotnet build`
- `dotnet run`
- Otwórz `http://localhost:5084/swagger/index.html`

## Opis
Endpointy:
- `GET /api/products` - Wyświetla całą listę produktów
- `GET /api/products/{id}` - Wyświetla dany produkt
- `POST /api/products` - Dodaje produkt do bazy danych
- `PUT /api/products/{id}` - Aktualizuje produkt istniejacy w bazie danych
- `DELETE /api/products/{id}` - Usuwa produkt z bazy danych

Dodatkowo zaimplementowałem mechanizm autoryzacji JWT.

- `POST /api/auth/register` - Rejestrowanie nowego użytkownika
- `POST /api/auth/login` - Logowanie

Endpointy do dodawania, usuwania, aktualizowania są zabezpieczone tokenem JWT, pozostałe
2 endpointy nie wymagają autoryzacji, ponieważ wydaje się to najbardziej intuicyjne.
