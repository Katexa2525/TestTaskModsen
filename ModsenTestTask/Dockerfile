#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ModsenTestTask/ModsenTestTask.csproj", "ModsenTestTask/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["DataAccess/Entities.csproj", "DataAccess/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Service/Service.csproj", "Service/"]
RUN dotnet restore "./ModsenTestTask/ModsenTestTask.csproj"
COPY . .
WORKDIR "/src/ModsenTestTask"
RUN dotnet build "./ModsenTestTask.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ModsenTestTask.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModsenTestTask.dll"]