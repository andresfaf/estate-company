# App Full Stack .NET + React + MongoDB
## Sistema para gestion de propiedades y propietarios

## 🚀 Tecnologías usadas
### Backend
- .NET 8
- Maspter -> Mapeo (Entities <> DTOs)
- MediatR
- NUnit
### Frontend
- React + Vite + Ts
- Material UI

### Databse
- MongoDB
- GridFS

## 📁 Estructura del proyecto backend
### ✅ RealEstate.API
Capa encargada de exponer los endpoints para el consumo de los clientes web, control de errores globales con middleware, datos de conexion de BD, DTOs para 
envio de datos del cliente web a endpoints.
````markdown
├── Controllers                     
├── Dtos                           
├── Middlewares
├── appsettings.json
├── Program.cs
````
### ✅ RealEstate.Application 
Capa encargada de ser la intermediaria entre capa de api e infraestructura, inplementa patron CQRS para el manejo de comandos de lectura y escritura, 
manejo de validaciones, mapeo de datos entre DTOs que se exponen a la capa api y entities de negocio y por medio de inyección de dependencias accede a los
repository para el acceso a datos.
````markdown
├── Commands    
│      ├── Moduls
│            ├── Action
│                   ├── Command
│                   ├── CommandHandler
├── Commom          → Ensamblaje para implementación de patron CQRS                  
├── DTOs
├── Exceptions
├── Queries
│      ├── Modul
│            ├── Action
│                   ├── Command
│                   ├── CommandHandler  
`````
### ✅ RealEstate.Domain
Capa encargada de manejar entidades del dominio o core del negocio, contiene los contratos del patron repository.
````markdown
├── Entities
├── Interfaces 
`````
### ✅ RealEstate.Infrastructure
Capa encargada del acceso a datos, implementa los contratos de la capa Domain, maneja el contexto de la base de datos.
````markdown
├── Data            → DbContexts
├── Repositories
`````
### ✅ RealEstate.Tests
Capa encargada de hacer test unitarios.

## 📁 Estructura del proyecto frontend
````markdown
├── src
│     ├── components                  → Componentes globales de la app
│     ├── features                    → Caracteristicas funcionales de la app
│           ├── Moduls
│                  ├── components     → Componentes del modulo
│                  ├── hooks          → Custons hook personalizados
│                  ├── pages          → Paginas del router
│                  ├── services       → Servicios de llamadas a la api
│                  └── types          → Tipos de datos
├── router                            → Enrutador de páginas
├── utils                             → archivos utilitarios                 
`````

## 🧠 Funcionalidades principales

- ✅ CRUD propietarios con foto
- ✅ Listar propiedades con foto activa
- ✅ Crear propiedades adjuntando imagenes y seleccinando la imagen activa
- ✅ Eliminar propiedad
- ✅ Agregar y listar rastros de la propiedad
- ✅ Filtros por nombre, direccion o rango de precios
- ✅ Ver detalle completo de la propiedad

## 🛠️ Cómo ejecutar el proyecto
### Clonar el repositorio
   ```bash
   https://github.com/andresfaf/estate-company.git
   ```

### Como ejecutar la base de datos
#### Restaurar backup
- Abre una consola
- Ejecuta el comando
 ```bash
   mongorestore --db RealEstateDb ruta_al_dump
   ```
ruta_al_dump es ubicación de la carpeta que esta  en la raiz del repositorio (RealEstateDb)
 
#### Ejecutar MongoDB instalado localmente
- Descarga MongoDB Community Server desde [https://www.mongodb.com/try/download/community](https://www.mongodb.com/try/download/community) según tu sistema operativo (Windows, macOS o Linux).
- Instala MongoDB siguiendo las instrucciones del instalador.
- En Windows, MongoDB suele instalarse como servicio y se inicia automáticamente.
- Si no es así, abre una terminal o consola y ejecuta el siguiente comando para iniciar el servidor MongoDB:

```bash
 mongod
```

MongoDB iniciará y escuchará en el puerto por defecto 27017.
Para conectarte al servidor MongoDB, puedes usar:
Mongo Shell con el comando:

```bash
mongo
```

O la herramienta gráfica MongoDB Compass.
#### Ejecutar MongoDB con Docker

Si tienes Docker instalado, puedes ejecutar MongoDB en un contenedor con el siguiente comando:

```bash
docker run -d --name mongodb -p 27017:27017 mongo
```

Esto descargará la imagen oficial de MongoDB y la ejecutará en un contenedor.
El puerto 27017 estará disponible para conexiones desde tu máquina local.

Para verificar que el contenedor está corriendo, usa:

```bash
docker ps
```

### Como ejecutar el backend
- Tener instalado SDK de .NET 8
- Si no es el caso, se puede descargar desde aqui: [https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Abrir la solución que se encuentra en la carpeta backend en la raiz del repositorio "RealEstate.sln"
- Tener ejecutado el MongoDb
- Ejecutar proyecto

### Como ejecutar el frontend
- Pararse desde una consola en la raiz de la carpeta frontend que esta en la raiz del repositorio
- Ejecutar npm install
- Tener ejecutado el backend
- Ejecutar npm run dev










