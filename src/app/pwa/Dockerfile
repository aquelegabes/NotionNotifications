FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

ENV ASPNETCORE_URLS=http://+:8000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/app/pwa/NotionNotifications.PWA.csproj", "src/app/pwa/"]
RUN dotnet restore "src/app/pwa/NotionNotifications.PWA.csproj"
COPY . .
WORKDIR "/src/src/app/pwa"
RUN dotnet build "NotionNotifications.PWA.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "NotionNotifications.PWA.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotionNotifications.PWA.dll"]
