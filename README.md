# 🍽️ SmartRecipes - Microservicio de Recetas Inteligentes

Este proyecto implementa un sistema distribuido con microservicios en .NET, que permite la gestión de recetas y el cálculo automático de calorías basado en sus ingredientes.
 El sistema incluye:

- 🧾 **RecetasService**: CRUD de recetas y sus ingredientes.
- 🔍 **AnalisisService**: Analiza y calcula calorías totales por receta.
- 🌐 **ApiGateway**: Encaminador central usando YARP para acceder a los servicios.

## 🚀 Tecnologías Utilizadas

- ASP.NET Core 9
- Docker + Docker Compose
- PostgreSQL
- Patrón Repository + Services
- API Gateway con YARP

---

## 🧱 Estructura del Proyecto

```bash
SmartRecipes/
├── ApiGateway/           # Redirecciona solicitudes a microservicios
├── RecetasService/       # Microservicio de recetas e ingredientes
├── AnalisisService/      # Microservicio para cálculo de calorías
├── docker-compose.yml    # Orquestación de servicios
└── README.md
```

---

### 🔧 Instrucciones para correr el proyecto
1. Clona el repositorio
    bash
    Copiar
    Editar
    git clone https://github.com/tuusuario/SmartRecipes.git
    cd SmartRecipes

2. Levanta los servicios con Docker
    bash
    Copiar
    Editar
    docker-compose up --build
    Accede a los servicios vía el API Gateway:

    Listar recetas: http://localhost:8080/recetas/api/recetas

    Obtener receta: http://localhost:8080/recetas/api/recetas/1

    Análisis: http://localhost:8080/analisis/api/analisis/1

#### 📦 Endpoints Principales
    RecetasService
    GET /api/recetas

    GET /api/recetas/{id}

    GET /api/ingredientes

    GET /api/ingredientes/{id}

    POST /api/recetas

    PUT /api/recetas/{id}

    AnalisisService
    GET /api/analisis/{recetaId} → Retorna calorías totales de una receta


