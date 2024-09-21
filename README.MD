# Teledoc 

Веб-сервис (API) для работы с клиентами, включая юридические лица (ЮЛ) и индивидуальных предпринимателей (ИП), с поддержкой операций CRUD. ЮЛ могут иметь нескольких учредителей.

- Разработка: ASP.NET Core
- Документация API: OpenAPI (Swagger)
- СУБД: MS SQL (на базе Docker)
- ORM: Entity Framework Core (Code First)

# Build 

Создание сертификата для HTTPS:

```bash

dotnet dev-certs https --trust

```

Экспорт сертификата:

```bash

dotnet dev-certs https --export-path ~/.aspnet/https/aspnetapp.pfx -p 12345678

```

Сборка и запуск контейнеров Docker:

```bash

docker-compose build
docker-compose up -d 

```
Применение миграций базы данных:

```bash 

dotnet ef database update --project Teledoc.Infrastructure/Teledoc.Infrastructure.csproj --startup-project Teledoc.API/Teledoc.API.csproj

```

Запуск API:

```bash

docker-compose up -d 

```

### Доступ к API

API доступен по адресу: https://localhost:7110/swagger/index.html

# Requirements

- .NET 8 
- Docker


# Features

- Clean Architecture 
- DDD
- Swagger Open API endpoint
- MediatR
- AutoMapper
- Dockerfile and Docker Compose (YAML) file for environmental setup

# TODO 

- CQRS implementation on Commands, Queries and Projections
