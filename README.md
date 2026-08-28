<div align="center">
  <h1>🎂 Tortas Yani - RESTful Backend API</h1>
  
  **API RESTful construida en .NET 8 Web API para la gestión del E-Commerce de Repostería Artesanal**

  [![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
  [![C#](https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://docs.microsoft.com/dotnet/csharp/)
  [![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-68217A?style=for-the-badge&logo=dotnet&logoColor=white)](https://docs.microsoft.com/ef/core/)
  [![SQLite](https://img.shields.io/badge/SQLite-Database-003B57?style=for-the-badge&logo=sqlite&logoColor=white)](https://www.sqlite.org/)
  [![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)](http://localhost:8080/swagger)
  [![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](LICENSE)

  ---
</div>

## 📌 Tabla de Contenidos

- [✨ Características de la API](#-características-de-la-api)
- [🛠️ Tecnologías y Arquitectura](#️-tecnologías-y-arquitectura)
- [📁 Estructura de la Solución](#-estructura-de-la-solución)
- [🚀 Guía de Instalación y Paso a Paso](#-guía-de-instalación-y-paso-a-paso)
- [📡 Endpoints Principales](#-endpoints-principales)
- [📄 Licencia](#-licencia)

---

## ✨ Características de la API

- 🔐 **Autenticación y Autorización JWT**: Seguridad con tokens JWT y hashing de contraseñas mediante BCrypt.
- 👥 **Gestión de Usuarios y Roles**: Manejo de roles `Admin` y `Client` con middleware personalizado de manejo de excepciones.
- 🍰 **Gestión del Catálogo de Productos**: Operaciones CRUD completas para productos, precios y categorías.
- 📑 **Documentación Interactiva Swagger**: UI de Swagger preconfigurada para probar todas las peticiones desde el navegador.
- 🗄️ **Base de Datos Ligera SQLite**: Configuración persistente con Entity Framework Core Code-First y Migraciones.

---

## 🛠️ Tecnologías y Arquitectura

| Componente | Tecnología |
| :--- | :--- |
| **Framework** | .NET 8.0 (ASP.NET Core Web API) |
| **ORM** | Entity Framework Core 8.0 |
| **Base de Datos** | SQLite (`tortasyani.db`) |
| **Seguridad** | JWT Bearer Tokens + BCrypt.Net-Next |
| **Documentación** | Swashbuckle / Swagger OpenAPI |
| **Manejo de Errores** | Custom Exception Handling Middleware |

---

## 📁 Estructura de la Solución

```text
TortasYaniAPI/
├── Controllers/              # Controladores REST (Auth, Products, Users, etc.)
├── DTOs/                     # Data Transfer Objects (Request/Response DTOs)
├── Data/                     # DbContext y configuraciones de Entity Framework
├── Middleware/               # Middleware de excepciones y autenticación
├── Migrations/               # Historial de migraciones de la Base de Datos
├── Models/                   # Entidades principales (User, Product, Order, etc.)
├── Services/                 # Lógica de negocio y servicios de repositorio
├── Properties/               # Configuración de lanzamiento (launchSettings.json)
├── appsettings.json          # Cadenas de conexión y parámetros de configuración
├── Program.cs                # Inicialización del pipeline de ASP.NET Core
└── TortasYaniAPI.csproj     # Archivo de proyecto .NET
```

---

## 🚀 Guía de Instalación y Paso a Paso

Sigue estos pasos para clonar, configurar y ejecutar el servidor Backend en tu entorno local:

### 1️⃣ **Requisitos Previos**
Asegúrate de contar con:
- **.NET 8.0 SDK** -> [Descargar .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Para verificar tu versión instalada:
```bash
dotnet --version
```

---

### 2️⃣ **Clonar el Repositorio**
```bash
git clone https://github.com/junn-shadow/TortasYaniAPIweb.git
cd TortasYaniAPIweb
```

---

### 3️⃣ **Restaurar Paquetes NuGet**
```bash
dotnet restore
```

---

### 4️⃣ **Ejecutar la API Backend**
Inicia la API en tu entorno local:
```bash
dotnet run
```

La API comenzará a escuchar peticiones en:
👉 `http://localhost:8080`

Para acceder a la documentación interactiva en **Swagger UI**:
👉 `http://localhost:8080/swagger`

---

## 📡 Endpoints Principales

### 🔑 Autenticación (`/api/Auth`)
- `POST /api/Auth/login` - Iniciar sesión y obtener Token JWT.
- `POST /api/Auth/register` - Registrar una nueva cuenta de cliente.

### 🍰 Productos (`/api/products`)
- `GET /api/products` - Obtener catálogo de productos.
- `GET /api/products/{id}` - Obtener detalle de un producto.
- `POST /api/products` - *(Admin)* Crear nuevo producto.
- `PUT /api/products/{id}` - *(Admin)* Actualizar producto.
- `DELETE /api/products/{id}` - *(Admin)* Eliminar producto.

### 👤 Usuarios (`/api/users`)
- `GET /api/users` - *(Admin)* Obtener lista de usuarios registrados.
- `PUT /api/users/{id}` - Actualizar perfil de usuario.
- `DELETE /api/users/{id}` - *(Admin)* Eliminar o desactivar usuario.

---

## 📄 Licencia

Este proyecto se distribuye bajo la Licencia **MIT**. Consulta el archivo `LICENSE` para más información.

<div align="center">
  <sub>Desarrollado con ❤️ para <b>Tortas Yani Backend API</b></sub>
</div>
