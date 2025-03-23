FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AnimeFranchises.Api/AnimeFranchises.Api.csproj", "AnimeFranchises.Api/"]
RUN dotnet restore "AnimeFranchises.Api/AnimeFranchises.Api.csproj"
COPY . .
WORKDIR "/src/AnimeFranchises.Api"
RUN dotnet build "AnimeFranchises.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AnimeFranchises.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimeFranchises.Api.dll"]
