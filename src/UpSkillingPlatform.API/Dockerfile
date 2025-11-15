FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/UpSkillingPlatform.API/UpSkillingPlatform.API.csproj", "src/UpSkillingPlatform.API/"]
COPY ["src/UpSkillingPlatform.Application/UpSkillingPlatform.Application.csproj", "src/UpSkillingPlatform.Application/"]
COPY ["src/UpSkillingPlatform.Domain/UpSkillingPlatform.Domain.csproj", "src/UpSkillingPlatform.Domain/"]
COPY ["src/UpSkillingPlatform.Infrastructure/UpSkillingPlatform.Infrastructure.csproj", "src/UpSkillingPlatform.Infrastructure/"]

RUN dotnet restore "src/UpSkillingPlatform.API/UpSkillingPlatform.API.csproj"

COPY . .

WORKDIR "/src/src/UpSkillingPlatform.API"
RUN dotnet build "UpSkillingPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UpSkillingPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpSkillingPlatform.API.dll"]
