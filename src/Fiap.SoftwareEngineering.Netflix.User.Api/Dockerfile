FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Fiap.SoftwareEngineering.Netflix.User.Api/Fiap.SoftwareEngineering.Netflix.User.Api.csproj", "src/Fiap.SoftwareEngineering.Netflix.User.Api/"]
RUN dotnet restore "src/Fiap.SoftwareEngineering.Netflix.User.Api/Fiap.SoftwareEngineering.Netflix.User.Api.csproj"
COPY . .
WORKDIR "/src/src/Fiap.SoftwareEngineering.Netflix.User.Api"
RUN dotnet build "Fiap.SoftwareEngineering.Netflix.User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fiap.SoftwareEngineering.Netflix.User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.SoftwareEngineering.Netflix.User.Api.dll"]