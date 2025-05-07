FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Directory.Build.props . 
COPY Directory.Packages.props . 

COPY ["OrderService/OrderService.WebApi/OrderService.WebApi.csproj", "OrderService/OrderService.WebApi/"]
COPY ["OrderService/OrderService.Application.Query/OrderService.Application.Query.csproj", "OrderService/OrderService.Application.Query/"]
COPY ["OrderService/OrderService.Domain/OrderService.Domain.csproj", "OrderService/OrderService.Domain/"]
COPY ["OrderService/OrderService.Infrastructure/OrderService.Infrastructure.csproj", "OrderService/OrderService.Infrastructure/"]
COPY ["OrderService/OrderService.Application.Command/OrderService.Application.Command.csproj", "OrderService/OrderService.Application.Command/"]
COPY ["OrderService/OrderService.QueryStack/OrderService.QueryStack.csproj", "OrderService/OrderService.QueryStack/"]
RUN dotnet restore "OrderService/OrderService.WebApi/OrderService.WebApi.csproj"
COPY . .
WORKDIR "/src/OrderService/OrderService.WebApi"
RUN dotnet build "OrderService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrderService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.WebApi.dll"]
