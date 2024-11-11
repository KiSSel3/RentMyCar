FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 10001
EXPOSE 10101

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/CarManagementService/CarManagementService.Presentation/CarManagementService.Presentation.csproj", "Services/CarManagementService/CarManagementService.Presentation/"]
COPY ["Services/CarManagementService/CarManagementService.Application/CarManagementService.Application.csproj", "Services/CarManagementService/CarManagementService.Application/"]
COPY ["Services/CarManagementService/CarManagementService.Domain/CarManagementService.Domain.csproj", "Services/CarManagementService/CarManagementService.Domain/"]
COPY ["Services/CarManagementService/CarManagementService.Infrastructure/CarManagementService.Infrastructure.csproj", "Services/CarManagementService/CarManagementService.Infrastructure/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "Services/CarManagementService/CarManagementService.Presentation/CarManagementService.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/CarManagementService/CarManagementService.Presentation"
RUN dotnet build "CarManagementService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CarManagementService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:10001
ENV ASPNETCORE_URLS=http://+:10101
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarManagementService.Presentation.dll"]
