FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Payments/Payments.Grpc/Payments.Grpc.csproj", "Payments/Payments.Grpc/"]
RUN dotnet restore "Payments/Payments.Grpc/Payments.Grpc.csproj"
COPY . .
WORKDIR "/src/Payments/Payments.Grpc"
RUN dotnet build "Payments.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payments.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payments.Grpc.dll"]
