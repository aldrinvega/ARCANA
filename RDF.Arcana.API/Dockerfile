FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RDF.Arcana.API/RDF.Arcana.API.csproj", "RDF.Arcana.API/"]
RUN dotnet restore "RDF.Arcana.API/RDF.Arcana.API.csproj"
COPY . .
WORKDIR "/src/RDF.Arcana.API"
RUN dotnet build "RDF.Arcana.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RDF.Arcana.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RDF.Arcana.API.dll"]
