using Microsoft.EntityFrameworkCore;
using TheSocial_Comments.Context;
using TheSocial_Comments.Extensions;
using TheSocial_Comments.Services;
using TheSocial_Comments.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//cors policy
builder.Services.AddCors(options => options.AddPolicy("mypolicy", build =>
{
    build.WithOrigins("https://localhost:7003");
    build.AllowAnyMethod();
    build.AllowAnyHeader();
}));
//Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICommentsInterface, CommentService>();
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("mypolicy");

app.MapControllers();

app.Run();
