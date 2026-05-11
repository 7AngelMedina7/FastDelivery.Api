# FastDelivery API — Backend

API REST construida con **.NET 8 **, **SQL Server** y **MongoDB**.
Las Bases de datos estan configuradas en Docker

---

> [!WARNING]  
> El archivo `.env` fue dejado fuera de `.gitignore` intencionalmente para mantener la misma configuración del BackEnd y poder hacer pruebas locales.  
> Igual que la configuración de `Connection strings` y el `Jwt Key`.

---

## Stack

| Capa                | Tecnología               |
| ------------------- | ------------------------ |
| Framework           | .NET 8 WebAPI            |
| Base de datos sql   | SQL Server 2022 (Docker) |
| Base de datos Mongo | MongoDB (Docker)         |
| ORM                 | EFC                      |
| Autenticación       | JWT Bearer               |

---

## Requisitos previos

- [Docker] instalado y corriendo
- [Visual Studio .NET 8]

---

## Pasos para correr el proyecto

### 1. Clonar el repositorio

```bash
git clone <https://github.com/7AngelMedina7/FastDelivery.Api.git>
cd fastdelivery.api
```

### 2. Verificar el archivo .env

El archivo `.env` está incluido en el repo. Verifica que exista:

```env
SA_PASSWORD=aZBqCYYwFTCVe4hBwv30vwAfQBgVGJTYjm8
```

Si no existe, créalo con la informacion de arriba para la contraseña de Docker.

### 3. Levantar las bases de datos con Docker
```bash
docker compose up -d
```

Apareceran los contenedores `fastdelivery-sql` y `fastdelivery-mongo`.

### 4. Correr el backend

```bash
cd FastDelivery.Api
dotnet run
```
>(Validar que el servidor arranca en `http://0.0.0.0:5000`)

Al arrancar, el proyecto automáticamente:

- Aplicara las migraciones de EF Core en SQL Server
- Creara las tablas necesarias
- Insertara datos de prueba (seed)

## Variables de entorno / Configuración

El archivo `appsettings.Development.json` ya está incluido en el repo con los valores necesarios.

| Variable          | Valor por defecto |
| ----------------- | ----------------- |
| SQL Server puerto | 1433              |
| MongoDB puerto    | 27017             |
| JWT Issuer        | FastDeliveryAPI   |

---

## Credenciales de prueba (seed)

Una vez que el proyecto arranca, ya hay datos listos para usar:

| Campo    | Valor               |
| -------- | ------------------- |
| Email    | `admin@example.com` |
| Password | `123456`            |

Usa estas credenciales en `POST /api/Auth/login` para obtener el JWT.

---

## Endpoints principales

| Método | Ruta                          | Descripción                        |
| ------ | ----------------------------- | ---------------------------------- |
| POST   | `/api/Auth/login`             | Obtener JWT                        |
| POST   | `/api/Auth/register`          | Registrar usuario                  |
| GET    | `/api/Auth/profile`           | Perfil del usuario actual          |
| GET    | `/api/Order/driver/my-orders` | Pedidos del repartidor |
| PATCH  | `/api/Order/{id}/status`      | Cambiar estado de una orden        |
| GET    | `/api/Order/{id}/history`     | Historial de movimientos de una orden          |

## Colección de Postman

Importar el archivo `FastDelivery.postman_collection.json` en Postman:

En la colección de Postman estaran todos los Endpoints de la aplicación.

---

## Estructura del proyecto

```
FastDelivery.Api/
├── Controllers/          # Endpoints
├── Services/             # Lógica
│   └── Interfaces/       # Contratos de servicios
├── Data/                 # DbContext y Seed de datos
├── Models/               # Entidades (User, Order, Client, OrderLog)
├── DTOs/
│   ├── Auth/
│   ├── Client/
│   ├── Order/
│   └── User/
├── Migrations/           # Migraciones
├── Config/               # Configuración de MongoDB
├── docker-compose.yml
├── appsettings.json
└── Program.cs
```
