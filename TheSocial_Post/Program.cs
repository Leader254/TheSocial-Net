using Microsoft.EntityFrameworkCore;
using TheSocial_Post.Context;
using TheSocial_Post.Extension;
using TheSocial_Post.Extensions;
using TheSocial_Post.Services;
using TheSocial_Post.Services.IService;

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

//Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// registering the base url for the client app
builder.Services.AddHttpClient("Comment", c=>c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentApi"]));
builder.Services.AddScoped<ICommentInterface, CommentsService>();

builder.Services.AddScoped<IPostService, PostService>();

//add Custom Services
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

app.MapControllers();

app.Run();
