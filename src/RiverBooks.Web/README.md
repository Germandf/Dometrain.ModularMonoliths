# RiverBooks.Web

### Books module

```bash
dotnet ef migrations add {NAME} -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
```

```bash
dotnet ef database update -c BookDbContext
```

### Users module

```bash
dotnet ef migrations add {NAME} -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
```

```bash
dotnet ef database update -c UsersDbContext
```