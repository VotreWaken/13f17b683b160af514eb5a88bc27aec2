# Teledoc
Веб-сервис (API) для работы с клиентами, включая юридические лица (ЮЛ) и индивидуальных предпринимателей (ИП), с поддержкой операций CRUD. ЮЛ могут иметь нескольких учредителей.

Разработка: ASP.NET Core
Документация API: OpenAPI (Swagger)
СУБД: MS SQL (на базе Docker)
ORM: Entity Framework Core (Code First)


# Build 

Перейдите в корневую директорию 

```
cd 13f17b683b160af514eb5a88bc27aec2/
```

запуск контейнеров Docker:

```bash

docker-compose

```

### Доступ к API

API доступен по адресу: https://localhost:7110/swagger/index.html

# Features

- Clean Architecture 
- DDD
- Swagger Open API endpoint
- MediatR
- CQRS implementation on Commands, Queries
- Dockerfile and Docker Compose (YAML) file for environmental setup
