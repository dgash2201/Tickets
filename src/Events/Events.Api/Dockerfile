FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Events/Events.Api/Events.Api.csproj", "Events/Events.Api/"]
RUN dotnet restore "Events/Events.Api/Events.Api.csproj"
COPY . .
WORKDIR "/src/Events/Events.Api"
RUN dotnet build "Events.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Events.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Events.Api.dll"]
