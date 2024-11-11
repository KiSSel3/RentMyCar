FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 10000
EXPOSE 10100

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/IdentityService/IdentityService.Presentation/IdentityService.Presentation.csproj", "Services/IdentityService/IdentityService.Presentation/"]
COPY ["Services/IdentityService/IdentityService.BLL/IdentityService.BLL.csproj", "Services/IdentityService/IdentityService.BLL/"]
COPY ["Services/IdentityService/IdentityService.DAL/IdentityService.DAL.csproj", "Services/IdentityService/IdentityService.DAL/"]
COPY ["Services/IdentityService/IdentityService.Domain/IdentityService.Domain.csproj", "Services/IdentityService/IdentityService.Domain/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "Services/IdentityService/IdentityService.Presentation/IdentityService.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/IdentityService/IdentityService.Presentation"
RUN dotnet build "IdentityService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IdentityService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_URLS=http://+:10100
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.Presentation.dll"]
