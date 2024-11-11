FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 10002
EXPOSE 10102

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/BookingService/BookingService.Presentation/BookingService.Presentation.csproj", "Services/BookingService/BookingService.Presentation/"]
COPY ["Services/BookingService/BookingService.BLL/BookingService.BLL.csproj", "Services/BookingService/BookingService.BLL/"]
COPY ["Services/BookingService/BookingService.DAL/BookingService.DAL.csproj", "Services/BookingService/BookingService.DAL/"]
COPY ["Services/BookingService/BookingService.Domain/BookingService.Domain.csproj", "Services/BookingService/BookingService.Domain/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "Services/BookingService/BookingService.Presentation/BookingService.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/BookingService/BookingService.Presentation"
RUN dotnet build "BookingService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookingService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:10002
ENV ASPNETCORE_URLS=http://+:10102
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService.Presentation.dll"]
