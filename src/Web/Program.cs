using Web.Interfaces;
using Web.Services;
using Web.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<EventsClient>();
builder.Services.AddScoped<OrganizationsClient>();
builder.Services.AddScoped<UsersClient>();
builder.Services.AddScoped<AuthClient>();
builder.Services.AddScoped<TicketsClient>();
builder.Services.AddScoped<PaymentsClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();