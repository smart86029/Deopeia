#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Viriplaca.Common/Viriplaca.Common.csproj", "src/Viriplaca.Common/"]
COPY ["src/Viriplaca.Common.Api/Viriplaca.Common.Api.csproj", "src/Viriplaca.Common.Api/"]
COPY ["src/Viriplaca.Common.App/Viriplaca.Common.App.csproj", "src/Viriplaca.Common.App/"]
COPY ["src/Viriplaca.Common.Data/Viriplaca.Common.Data.csproj", "src/Viriplaca.Common.Data/"]
COPY ["src/Viriplaca.HR.Api/Viriplaca.HR.Api.csproj", "src/Viriplaca.HR.Api/"]
COPY ["src/Viriplaca.HR.App/Viriplaca.HR.App.csproj", "src/Viriplaca.HR.App/"]
COPY ["src/Viriplaca.HR.Data/Viriplaca.HR.Data.csproj", "src/Viriplaca.HR.Data/"]
COPY ["src/Viriplaca.HR.Domain/Viriplaca.HR.Domain.csproj", "src/Viriplaca.HR.Domain/"]
RUN dotnet restore "./src/Viriplaca.HR.Api/Viriplaca.HR.Api.csproj"
COPY . .
WORKDIR "/src/src/Viriplaca.HR.Api"
RUN dotnet build "./Viriplaca.HR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Viriplaca.HR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Viriplaca.HR.Api.dll"]
