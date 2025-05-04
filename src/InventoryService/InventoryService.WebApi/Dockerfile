FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 1001
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InventoryService/InventoryService.WebApi/InventoryService.WebApi.csproj", "InventoryService/InventoryService.WebApi/"]
COPY ["InventoryService/InventoryService.Application.Command/InventoryService.Application.Command.csproj", "InventoryService/InventoryService.Application.Command/"]
COPY ["InventoryService/InventoryService.Domain/InventoryService.Domain.csproj", "InventoryService/InventoryService.Domain/"]
COPY ["InventoryService/InventoryService.Infrastructure/InventoryService.Infrastructure.csproj", "InventoryService/InventoryService.Infrastructure/"]
RUN dotnet restore "InventoryService/InventoryService.WebApi/InventoryService.WebApi.csproj"
COPY . .
WORKDIR "/src/InventoryService/InventoryService.WebApi"
RUN dotnet build "InventoryService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InventoryService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.WebApi.dll"]
