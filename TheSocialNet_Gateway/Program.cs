using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TheSocialNet_Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppAuthentication();

//ocelot Configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

//cors
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{

    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));
var app = builder.Build();
app.UseCors("policy1");
app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
