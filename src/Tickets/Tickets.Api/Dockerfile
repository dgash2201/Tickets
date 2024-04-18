FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tickets/Tickets.Api/Tickets.Api.csproj", "Tickets/Tickets.Api/"]
RUN dotnet restore "Tickets/Tickets.Api/Tickets.Api.csproj"
COPY . .
WORKDIR "/src/Tickets/Tickets.Api"
RUN dotnet build "Tickets.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tickets.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tickets.Api.dll"]
