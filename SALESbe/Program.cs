using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// CORS
string[] allowedEndpoints = configuration.GetSection("Client:Endpoints").GetChildren().Select(x => x.Value ?? string.Empty).ToArray();

builder.Services.AddCors(cors =>
{
    cors.AddPolicy(name: "AllowAll",
    options =>
    {
        options.WithOrigins(allowedEndpoints);
        options.SetIsOriginAllowed(host => true);
        options.AllowAnyMethod();
        options.AllowAnyHeader();
        options.AllowCredentials();

    });
});
// CORS

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
