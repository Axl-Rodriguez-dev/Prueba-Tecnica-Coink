# Prueba Técnica Coink — API .NET

Este repositorio contiene una API REST para la prueba técnica de Coink. A continuación se explica únicamente la estructura del proyecto y cómo ejecutarlo.

## Estructura

- `Prueba-Tecnica-Coink.sln` — solución de .NET
- `API/` — proyecto principal de la API
  - `API/Program.cs` — punto de entrada
  - `API/appsettings.json` y `API/appsettings.Development.json` — configuración
  - `API/Controllers/*.cs` — controladores HTTP (Country, Department, Municipality, User)
  - `API/DTOs/*.cs` — objetos de transferencia de datos
  - `API/Entities/*.cs` — entidades de dominio
  - `API/Data/AppDbContext.cs` — DbContext de EF Core
  - `API/Services/*.cs` — servicios de negocio
  - `API/Interfaces/*.cs` — contratos de servicios
  - `API/Helpers/*.cs` — utilidades (por ejemplo AutoMapper)
  - `API/Migrations/*` — migraciones de base de datos

## Cómo ejecutarlo

1. Requisitos mínimos

- .NET SDK (target `net10.0`)
- Base de datos PostgreSQL en local

2. Configura la conexión a base de datos en `API/appsettings.json` (ajusta credenciales y nombre de BD):

```json
{
  "ConnectionStrings": {
    "Connection": "Host=localhost;Port=5432;Database=coink;Username=postgres;Password=admin"
  }
}
```

3. Restaura y compila la solución:

```sh
dotnet restore
dotnet build
```

4. (Opcional) Aplica migraciones si aún no existe la base de datos:

```sh
dotnet ef database update --project API
```

5. Ejecuta la API:

```sh
dotnet run --project API
```
