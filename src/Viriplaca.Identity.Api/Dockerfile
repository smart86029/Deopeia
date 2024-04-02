#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Viriplaca.Common/Viriplaca.Common.csproj", "src/Viriplaca.Common/"]
COPY ["src/Viriplaca.Common.Api/Viriplaca.Common.Api.csproj", "src/Viriplaca.Common.Api/"]
COPY ["src/Viriplaca.Common.App/Viriplaca.Common.App.csproj", "src/Viriplaca.Common.App/"]
COPY ["src/Viriplaca.Common.Data/Viriplaca.Common.Data.csproj", "src/Viriplaca.Common.Data/"]
COPY ["src/Viriplaca.Identity.Api/Viriplaca.Identity.Api.csproj", "src/Viriplaca.Identity.Api/"]
COPY ["src/Viriplaca.Identity.App/Viriplaca.Identity.App.csproj", "src/Viriplaca.Identity.App/"]
COPY ["src/Viriplaca.Identity.Data/Viriplaca.Identity.Data.csproj", "src/Viriplaca.Identity.Data/"]
COPY ["src/Viriplaca.Identity.Domain/Viriplaca.Identity.Domain.csproj", "src/Viriplaca.Identity.Domain/"]
RUN dotnet restore "./src/Viriplaca.Identity.Api/Viriplaca.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Viriplaca.Identity.Api"
RUN dotnet build "./Viriplaca.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Viriplaca.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Viriplaca.Identity.Api.dll"]
