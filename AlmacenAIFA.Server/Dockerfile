FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Copiamos desde la raíz de la carpeta Server
COPY ["AlmacenAIFA.Server.csproj", "."]
RUN dotnet restore "AlmacenAIFA.Server.csproj"
COPY . .
RUN dotnet publish "AlmacenAIFA.Server.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "AlmacenAIFA.Server.dll"]