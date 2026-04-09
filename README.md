# 📚 LibraryAPI

API REST desarrollada en **.NET 10** con **C#** para la gestión de libros y autores. Proyecto de prueba técnica para desarrollador .NET.

---

## 🛠️ Tecnologías utilizadas

| Tecnología | Versión |
|---|---|
| .NET | 10 |
| C# | 13 |
| ASP.NET Core Web API | 10 |
| Entity Framework Core | 9.x |
| SQL Server | 2019 o superior |
| Swagger / OpenAPI | Integrado |

---

## 📋 Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) con usuario `sa` habilitado
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) o VS Code
- [Postman](https://www.postman.com/) para probar los endpoints (opcional)

---

## 🗂️ Estructura del proyecto

```
LibraryAPI/
├── Controllers/
│   ├── AutoresController.cs       # Endpoints CRUD de autores
│   └── LibrosController.cs        # Endpoints CRUD de libros
├── DTOs/
│   ├── AuthorDTO.cs               # Objetos de transferencia para autores
│   └── BookDTO.cs                 # Objetos de transferencia para libros
├── Entities/
│   ├── Author.cs                  # Entidad Autor
│   └── Book.cs                    # Entidad Libro
├── Exceptions/
│   ├── AuthorException.cs
│   └── BookException.cs
├── Interfaces/
│   ├── IAuthorService.cs
│   └── IBookService.cs
├── Services/
│   ├── AuthorService.cs           # Lógica de negocio de autores
│   └── BookService.cs             # Lógica de negocio de libros
├── Properties/
│   └── launchSettings.json
├── ApplicationDbContext.cs    # Contexto de base de datos (EF Core)
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```

---

## ⚙️ Configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/LibraryAPI.git
cd LibraryAPI
```

### 2. Configurar la cadena de conexión

Edita el archivo `appsettings.json` con los datos de tu instancia de SQL Server:
La base de datos se encuentra con Windows Authentication
```json
{
  "ConnectionStrings": {
  	"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;"
},
  "MaxLibrosPermitidos": 10
}
```

> ⚠️ **Nota de seguridad:** No subas credenciales reales a GitHub. Usa `appsettings.Development.json` o variables de entorno para datos sensibles en producción.

### 3. Instalar dependencias

```bash
dotnet restore
```

### 4. Crear la base de datos

Ejecuta las migraciones para crear las tablas automáticamente:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> Si no tienes EF Tools instalado globalmente:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### 5. Ejecutar el proyecto

```bash
dotnet run
```

La API quedará disponible en:
```
http://localhost:5025
```

Swagger UI disponible en:
```
http://localhost:5025/openapi/v1.json
```

---

## 🗃️ Modelo de datos

### Autor
| Campo | Tipo | Requerido |
|---|---|---|
| Id | int | Auto (PK) |
| NombreCompleto | string | ✅ |
| FechaNacimiento | DateTime | ✅ |
| CiudadProcedencia | string | ✅ |
| CorreoElectronico | string | ✅ |

### Libro
| Campo | Tipo | Requerido |
|---|---|---|
| Id | int | Auto (PK) |
| Titulo | string | ✅ |
| Anio | int | ✅ |
| Genero | string | ✅ |
| NumeroPaginas | int | ✅ |
| AutorId | int | ✅ (FK) |

---

## 📡 Endpoints disponibles

### Autores — `/api/author`

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/api/author` | Obtener todos los autores |
| GET | `/api/author/{id}` | Obtener autor por ID |
| POST | `/api/author` | Crear un nuevo autor |
| PUT | `/api/author/{id}` | Actualizar un autor |
| DELETE | `/api/author/{id}` | Eliminar un autor |

### Libros — `/api/book`

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/api/book` | Obtener todos los libros |
| GET | `/api/book/{id}` | Obtener libro por ID |
| POST | `/api/book` | Crear un nuevo libro |
| PUT | `/api/book/{id}` | Actualizar un libro |
| DELETE | `/api/book/{id}` | Eliminar un libro |

---

## 📦 Ejemplos de uso

### Crear un autor

```http
POST /api/author
Content-Type: application/json

{
  "nombreCompleto": "Gabriel García Márquez",
  "fechaNacimiento": "1927-03-06",
  "ciudadProcedencia": "Aracataca",
  "correoElectronico": "ggarcia@literatura.com"
}
```

### Crear un libro

```http
POST /api/book
Content-Type: application/json

{
  "titulo": "Cien Años de Soledad",
  "anio": 1967,
  "genero": "Realismo Mágico",
  "numeroPaginas": 471,
  "autorId": 1
}
```

---

## 📏 Reglas de negocio

- Todos los campos marcados como requeridos son obligatorios.
- No se puede registrar un libro si el autor no existe → responde con: `"El autor no está registrado."`
- Existe un límite máximo de libros configurado en `appsettings.json` (`MaxLibrosPermitidos`). Si se supera → responde con: `"No es posible registrar el libro, se alcanzó el máximo permitido."`
- Se garantiza la integridad referencial: no se puede eliminar un autor que tiene libros asociados.

---

## 🏗️ Arquitectura

El proyecto sigue una arquitectura en capas con separación de responsabilidades:

```
Request → Controller → Interface → Service → DbContext → SQL Server
                                     ↓
                                 DTOs / Exceptions
```

- **Controllers:** Reciben las solicitudes HTTP y devuelven respuestas.
- **Interfaces:** Contratos que desacoplan la implementación del consumidor.
- **Services:** Contienen toda la lógica de negocio.
- **DTOs:** Objetos de transferencia que evitan exponer las entidades directamente.
- **Entities:** Modelos que mapean las tablas de la base de datos.
- **Exceptions:** Excepciones personalizadas para las reglas de negocio.

---

## 🔒 Variables de entorno (producción)

Para no exponer credenciales en el repositorio, usa variables de entorno en producción:

```bash
export ConnectionStrings__DefaultConnection="Server=...;Database=...;"
export MaxLibrosPermitidos=10
```

---

## 👤 Juan Pablo velásquez Moreno

Desarrollado como prueba técnica para desarrollador .NET.
