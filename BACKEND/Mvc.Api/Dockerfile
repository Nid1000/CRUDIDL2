FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos toda la solución del backend
COPY BACKEND/. .

# Restaurar dependencias y compilar en modo Release
RUN dotnet restore "Mvc.Api/Mvc.Api.csproj"
RUN dotnet publish "Mvc.Api/Mvc.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Puerto donde expone la API
ENV ASPNETCORE_URLS=http://0.0.0.0:5133
EXPOSE 5133

ENTRYPOINT ["dotnet", "Mvc.Api.dll"]

