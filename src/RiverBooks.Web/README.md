# RiverBooks.Web

### Books module

```bash
dotnet ef migrations add {NAME} -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
```

### Users module

```bash
dotnet ef migrations add {NAME} -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
```