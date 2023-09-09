using Microsoft.AspNetCore.Identity;
using TheSocial_Auth.Context;
using Microsoft.EntityFrameworkCore;
using TheSocial_Auth.Extensions;
using TheSocial_Auth.Services.IService;
using TheSocial_Auth.Services;
using TheSocial_Auth.Utility;
using TheSocial_Auth.Models;
using TheMessage_Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//add identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


//register services
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IMessageBus, MessageBus>();

// register automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configure jwt
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
