# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy everything (context is solution root)
COPY . .

# Restore & publish only the API project
RUN dotnet restore "StockManager.Api/StockManager.Api.csproj"
RUN dotnet publish "StockManager.Api/StockManager.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "StockManager.Api.dll"]
