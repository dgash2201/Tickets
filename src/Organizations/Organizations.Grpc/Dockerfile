FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Organizations/Organizations.Grpc/Organizations.Grpc.csproj", "Organizations/Organizations.Grpc/"]
RUN dotnet restore "Organizations/Organizations.Grpc/Organizations.Grpc.csproj"
COPY . .
WORKDIR "/src/Organizations/Organizations.Grpc"
RUN dotnet build "Organizations.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Organizations.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Organizations.Grpc.dll"]
