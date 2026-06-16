# repositorio : https://github.com/Adrianradap/Progra-proyecto
# Doraemon — Proyecto Final Programacion II

**Adrian Rada | Eito Ygei**


## Estructura

```
doraemon-proyecto/
├── doraemon-backend/          # API REST .NET 10
│   ├── Controllers/
│   │   └── CriaturasController.cs
│   ├── Datos/
│   │   └── AppDbContext.cs     puente a la BD
│   ├── Migrations/            crea la tabla automaticamente
│   ├── Models/
│   │   └── Criatura.cs
│   ├── Negocio/
│   │   └── CriaturaService.cs
│   ├── appsettings.json       connection string SQLite
│   └── Program.cs
│
└── doraemon-frontend/         <-Angular SPA
    └── src/app/
        ├── core/http/api-client.ts
        ├── criaturas/
        │   ├── criatura.ts          (interfaces)
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
# La base de datos criaturas.db se crea automaticamente 
# Por defecto esta la direccion esta en puerto 5000 si al momento de la creacion sale otro puerto debera de eliminar la carpeta migraciones y cambiar la direccion en los archivos del front end
# frontend -> src -> app-> criaturas -> todos los criaturas.ts cambiar el puerto al que le salio en backend
```

### Frontend

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
- se crean 5 ciratuarsa la primera vez

