# 🍽️ SmartRecipes - Microservicio de Recetas, Calorías y Usuarios

Este proyecto implementa un sistema distribuido con microservicios en .NET, que permite la gestión de recetas, el cálculo automático de calorías basado en sus ingredientes y la administración de usuarios y sus recetas favoritas.

El sistema incluye:

* 💾 **RecetasService**: CRUD de recetas y sus ingredientes.
* 🔍 **AnalisisService**: Analiza y calcula calorías totales por receta.
* 👤 **UsuariosService**: Registro y autenticación de usuarios, gestión de recetas favoritas.
* 🌐 **ApiGateway**: Encaminador central usando YARP para acceder a los servicios.
* 💾 Base de datos: PostgreSQL como motor de persistencia para cada microservicio.

## 🚀 Tecnologías Utilizadas

* ASP.NET Core 9
* Docker + Docker Compose
* PostgreSQL
* Patrón Repository + Services
* API Gateway con YARP

---

## 🧱 Estructura del Proyecto

```bash
SmartRecipesApp/
├── ApiGateway/           # Redirecciona solicitudes a microservicios
├── RecetasService/       # Microservicio de recetas e ingredientes
├── AnalisisService/      # Microservicio para cálculo de calorías
├── UsuariosService/      # Microservicio de usuarios y recetas favoritas
├── docker-compose.yml    # Orquestación de servicios
└── README.md
```

---

### 🔧 Instrucciones para correr el proyecto

1. Clona el repositorio

```bash
git clone https://github.com/JoelIRR/SmartRecipesApp.git
```

2. Levanta los servicios con Docker

```bash
cd SmartRecipesApp
docker-compose up --build
```

3. Accede a los servicios vía el API Gateway:

Listar recetas:

```bash
http://localhost:8002/api/recetas
```

Obtener receta:

```bash
http://localhost:8002/api/recetas/{id-receta}
```

Análisis Calorías:

```bash
http://localhost:8003/api/analisisService/{id-receta}
```

Listar usuarios:
```bash
http://localhost:8004/api/Usuarios/listado
```

Recetas favoritas de usuarios:

```bash
http://localhost:8004/api/Usuarios/{id-user}/favoritas
```

---

## 📦 Endpoints Principales

### 📂 RecetasService

#### Recetas

* `GET /api/recetas` → Lista todas las recetas disponibles.
* `GET /api/recetas/{id}` → Obtiene los detalles de una receta específica por ID.
* `POST /api/recetas` → Crea una nueva receta.
* `PUT /api/recetas/{id}` → Actualiza los datos de una receta existente por ID.
* `DELETE /api/recetas/{id}` → Elimina una receta por su ID.

#### Ingredientes

* `GET /api/ingredientes` → Lista todos los ingredientes disponibles.
* `GET /api/ingredientes/{id}` → Obtiene los detalles de un ingrediente específico por ID.
* `POST /api/ingredientes` → Crea un nuevo ingrediente.
* `PUT /api/ingredientes/{id}` → Actualiza un ingrediente existente por ID.
* `DELETE /api/ingredientes/{id}` → Elimina un ingrediente por su ID.

### 🔍 AnalisisService

* `GET /api/analisis/{recetaId}` → Retorna las calorías totales de una receta.

### 👤 UsuariosService

#### Autenticación y Usuarios

* `POST /api/usuarios/registrar` → Registra un nuevo usuario.
* `POST /api/usuarios/login` → Autentica un usuario existente.

#### Recetas Favoritas

* `POST /api/usuarios/{usuarioId}/favoritas/{recetaId}` → Agrega una receta a favoritos.
* `GET /api/usuarios/{usuarioId}/favoritas` → Lista todas las recetas favoritas por ID de usuario.
* `DELETE /api/usuarios/{usuarioId}/favoritas/{recetaId}` → Elimina una receta favorita.
* `GET /api/usuarios/{usuarioId}/favoritas/detalle` → Muestra el nombre del usuario y los nombres de las recetas favoritas.
* `GET /api/usuarios/listado` → Muestra todos los usuarios y sus correros.

---

## 📝 Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.
