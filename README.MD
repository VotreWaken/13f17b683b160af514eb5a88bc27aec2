# Build 

Создайте сертификат для работы с HTTPS протоколом

```bash

dotnet dev-certs https --trust

```

```bash

dotnet dev-certs https --export-path ~/.aspnet/https/aspnetapp.pfx -p 12345678

```

start Docker Containers from YAML file by running:

```bash

docker-compose

```

Navigate into the \foodtruacker.Framework\Authentication directory and run the following command:

```bash 

dotnet ef database update

```

API Working on https://localhost:7110/swagger/index.html

# Requirements

- .NET 8 
- Docker


# Features

- Clean Architecture 
- DDD
- Swagger Open API endpoint
- MediatR
- AutoMapper
[-] CQRS implementation on Commands, Queries and Projections
- Dockerfile and Docker Compose (YAML) file for environmental setup