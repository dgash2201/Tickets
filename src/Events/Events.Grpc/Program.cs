using Events.Application;
using Events.Grpc.Services;
using Events.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();