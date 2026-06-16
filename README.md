# Doraemon — Proyecto Final Programacion II

**Adrian Rada | Eito Ygei**

Proyecto reorganizado siguiendo el patron de tiendaweb: separacion en `doraemon-backend` (.NET + SQLite) y `doraemon-frontend` (Angular SPA).

---

## Estructura

```
doraemon-proyecto/
├── doraemon-backend/          # API REST .NET 10
│   ├── Controllers/
│   │   └── CriaturasController.cs
│   ├── Datos/
│   │   └── AppDbContext.cs    ← NUEVO: puente a la BD
│   ├── Migrations/            ← NUEVO: crea la tabla automaticamente
│   ├── Models/
│   │   └── Criatura.cs
│   ├── Negocio/
│   │   └── CriaturaService.cs
│   ├── appsettings.json       ← NUEVO: connection string SQLite
│   └── Program.cs
│
└── doraemon-frontend/         ← NUEVO: Angular SPA
    └── src/app/
        ├── core/http/api-client.ts
        ├── criaturas/
        │   ├── criatura.ts          (interfaces/DTOs)
        │   ├── criatura-lista.ts    (ver + atacar + eliminar)
        │   ├── criatura-lista.html
        │   ├── criatura-crear.ts    (crear nueva)
        │   └── criatura-crear.html
        ├── nav-menu/
        └── app.ts / app.routes.ts / app.config.ts
```

---

## Como correr

### Backend
```bash
cd doraemon-backend
dotnet run
# Corre en http://localhost:5056
# La base de datos criaturas.db se crea automaticamente
```

### Frontend
```bash
cd doraemon-frontend
npm install
ng serve
# Corre en http://localhost:4200
```

---

## Base de datos

- **Motor:** SQLite (archivo `criaturas.db` en la carpeta del backend)
- **Tabla:** `Criaturas`
- **Columnas:** Id, Nombre, Tipo, Hp, Atk, Def
- **Seed:** Las 5 criaturas iniciales se insertan automaticamente la primera vez

---

## Cambios realizados (vs prograsimplesito original)

| Archivo | Cambio |
|---------|--------|
| `Datos/AppDbContext.cs` | **NUEVO** — DbContext con EF Core + datos semilla |
| `Migrations/` | **NUEVO** — migración que crea tabla `Criaturas` |
| `appsettings.json` | **NUEVO** — connection string `criaturas.db` |
| `Program.cs` | Registra `AppDbContext` con SQLite; `CriaturaService` cambia a `Scoped`; aplica migraciones al arrancar |
| `Negocio/CriaturaService.cs` | Recibe `AppDbContext` por inyeccion; reemplaza la lista en memoria por llamadas a `_db.Criaturas`; agrega `_db.SaveChanges()` en Crear, Eliminar y SimularAtaque |
| `Models/Criatura.cs` | Sin cambios |
| `Controllers/CriaturasController.cs` | Sin cambios |
| `doraemon-frontend/` | **NUEVO** — SPA Angular con los mismos 5 endpoints |
