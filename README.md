# CRUD (Personas / Casas / Mascotas) - Backend .NET 9 + Frontend Angular

- **Backend**: ASP.NET Core Web API (.NET 9) + Entity Framework Core + MySQL  
- **Frontend**: Angular (standalone components, Angular 17+)  

---

## 1. Requisitos en cualquier PC

### Backend

- **.NET SDK 9.0**  
  Descargar desde: https://dotnet.microsoft.com/es-es/download/dotnet

- **MySQL Server 8.x** (o compatible)  
  - Host: `localhost`  
  - Puerto: `3306`  
  - Usuario: `root`  
  - **Sin contraseña** (si se usa contraseña, ajustar la cadena de conexión).

### Frontend

- **Node.js** (versión LTS recomendada, por ejemplo 20.x)  
  Descargar desde: https://nodejs.org

- (Opcional) **Angular CLI** global:

```bash
npm install -g @angular/cli
```

---

## 2. Preparar la base de datos MySQL

1. Crear la base de datos:

```sql
CREATE DATABASE IF NOT EXISTS demo_db;
USE demo_db;
```

2. Crear tablas y datos iniciales:

- Puedes ejecutar el script `demo_db.sql` de la raíz del proyecto **o**  
- Ejecutar `BACKEND/Documentacion/Tablas.sql` y luego insertar los tipos de documento.

Los tipos de documento mínimos (para evitar errores de llave foránea) son:

```sql
INSERT INTO persona_tipo_documento (id, codigo, descripcion, user_create)
VALUES (1, 'DNI', 'DNI', 1)
ON DUPLICATE KEY UPDATE id = id;

INSERT INTO persona_tipo_documento (id, codigo, descripcion, user_create)
VALUES (2, 'CE', 'Carnet de extranjería', 1)
ON DUPLICATE KEY UPDATE id = id;
```

---

## 3. Configuración de la cadena de conexión

Archivo: `BACKEND/Mvc.Api/appsettings.Development.json`

```json
"ConnectionStrings": {
  "demoDb": "Server=localhost;Port=3306;Database=demo_db;Uid=root;Pwd=;SslMode=None;AllowPublicKeyRetrieval=True;"
}
```

Si el usuario `root` tiene contraseña en otra PC, solo cambia `Pwd=` por esa contraseña.

---

## 4. Ejecutar el backend

Desde una terminal en la carpeta raíz del proyecto:

```bash
cd BACKEND/Mvc.Api
dotnet run
```

- El backend se levanta por defecto en:  
  `http://localhost:5133`

- Endpoints principales:
  - `GET/POST/PUT/DELETE /api/persona`
  - `GET/POST/PUT/DELETE /api/casa`
  - `GET/POST/PUT/DELETE /api/mascotatipo`
  - `GET/POST/PUT/DELETE /api/mascota`

---

## 5. Ejecutar el frontend

Desde otra terminal:

```bash
cd FRONTEND/crud_angular
npm install        # solo la primera vez
npx ng serve       # o: ng serve
```

- El frontend queda disponible en:  
  `http://localhost:4200`

---

## 6. Flujo completo

1. Levantar MySQL y asegurarse de que existe la base `demo_db` con las tablas y tipos de documento.
2. Ejecutar el **backend** (`dotnet run` en `BACKEND/Mvc.Api`).
3. Ejecutar el **frontend** (`npx ng serve` en `FRONTEND/crud_angular`).
4. Abrir en el navegador:
   - `http://localhost:4200` para la UI Angular.
   - `http://localhost:5133/api/persona` para probar la API directamente.

---

## 7. Solución al error: "Missing script: start"

Si usted estaba parado en la carpeta raíz y ejecutaba:

```bash
npm install
npm run start
```

le salía **Missing script: start** porque **la raíz no tenía package.json**.

Ahora el proyecto ya incluye un `package.json` en la raíz, y puede hacer:

```bash
npm run install:frontend
npm run start
```


