# IxClouds

Sistema de gestión de inventario y ventas desarrollado con arquitectura limpia usando ASP.NET Core y Angular.

---

## Vista General

IxClouds es una aplicación orientada a la administración de productos, control de inventario y registro de ventas en tiempo real.  
El sistema permite agregar, editar y eliminar productos, controlar automáticamente el stock después de cada venta y visualizar estadísticas generales del negocio.

El proyecto está construido separando responsabilidades mediante Clean Architecture para mantener un código escalable, mantenible y profesional.

---

# Tecnologías Utilizadas

### Backend

![.NET](https://img.shields.io/badge/.NET_9-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-68217A?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)

### Frontend

![Angular](https://img.shields.io/badge/Angular_20-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![Angular Material](https://img.shields.io/badge/Angular_Material-3F51B5?style=for-the-badge&logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)

### Herramientas

![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)
![VS Code](https://img.shields.io/badge/VS_Code-007ACC?style=for-the-badge&logo=visualstudiocode&logoColor=white)

---

# Arquitectura del Proyecto

<div align="center">

```txt
IxClouds
│
├── IxClouds.API
├── IxClouds.Domain
├── IxCloud.DataAccess
├── IxClouds.Frontend
└── IxClouds.slnx

Estructura Interna
Proyecto	Responsabilidad	Tecnologías

IxClouds.API Exposición de endpoints y configuración general	ASP.NET Core, Swagger

IxClouds.Domain	Lógica de negocio y contratos	C#, Interfaces, Services

IxCloud.DataAccess	Persistencia y acceso a datos	Entity Framework Core, SQL Server

IxClouds.Frontend	Interfaz visual y experiencia de usuario	Angular, Angular Material

Flujo General del Sistema

┌─────────────────────┐
│     Frontend        │
│      Angular        │
└─────────┬───────────┘
          │ HTTP Requests
          ▼
┌─────────────────────┐
│    ASP.NET API      │
│   Controllers/API   │
└─────────┬───────────┘
          │ Services
          ▼
┌─────────────────────┐
│       Domain        │
│  Business Logic     │
└─────────┬───────────┘
          │ Repositories
          ▼
┌─────────────────────┐
│    Data Access      │
│ Entity Framework    │
└─────────┬───────────┘
          │
          ▼
┌─────────────────────┐
│     SQL Server      │
└─────────────────────┘
</div>
Funcionalidades Principales
Gestión de Inventario
<div align="center">
Funcionalidad	Estado
Registro de productos	Completo
Edición de productos	Completo
Eliminación de productos	Completo
Búsqueda dinámica	Completo
Control de stock	Completo
Alertas de bajo stock	Completo
</div>
Gestión de Ventas
<div align="center">
Funcionalidad	Estado
Registro de ventas	Completo
Descuento automático de stock	Completo
Historial de ventas	Completo
Totalización automática	Completo
Visualización de detalles	Completo
</div>
Panel Administrativo
<div align="center">
Estadística	Disponible
Ventas totales	Sí
Ventas del día	Sí
Total de inventario	Sí
Productos más vendidos	Sí
Alertas visuales	Sí
</div>
Características Técnicas
<div align="center">
Arquitectura	Backend	Frontend	Base de Datos
Clean Architecture	ASP.NET Core	Angular 20	SQL Server
Repository Pattern	Entity Framework	Angular Material	Migrations
Dependency Injection	AutoMapper	Standalone Components	Seeders
</div>
Tecnologías Utilizadas
Backend








Frontend






Herramientas








Ejecución del Proyecto
Backend
cd IxClouds.API
dotnet restore
dotnet run
<div align="center">
https://localhost:xxxx
</div>
Frontend
cd IxClouds.Frontend
npm install
ng serve
<div align="center">
http://localhost:4200
</div>
Roadmap
<div align="center">
[✔] Sistema de inventario
[✔] Sistema de ventas
[✔] Persistencia SQL Server
[✔] Arquitectura limpia
[✔] Dashboard administrativo

[ ] Autenticación JWT
[ ] Roles y permisos
[ ] Estadísticas gráficas
[ ] Reportes PDF
[ ] Exportación Excel
[ ] Gestión de clientes
[ ] Gestión de proveedores
[ ] Modo oscuro
[ ] Responsive avanzado
</div>
Estado del Proyecto
<div align="center">

IxClouds actualmente cuenta con una base sólida y completamente funcional para administración de inventario y ventas.

El sistema incluye persistencia de datos, arquitectura escalable, interfaz moderna y comunicación completa entre frontend y backend.

</div>
Autor
<div align="center">
Sebastian López

Backend Developer • Frontend Developer

ASP.NET Core • Angular • SQL Server

</div>
Licencia
<div align="center">

MIT License

</div> ```
