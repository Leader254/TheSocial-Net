using Microsoft.EntityFrameworkCore;
using TheSocial_Post.Context;
using TheSocial_Post.Extension;
using TheSocial_Post.Extensions;
using TheSocial_Post.Services;
using TheSocial_Post.Services.IService;
using TheSocial_Post.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

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
builder.Services.AddScoped<Util>();
builder.Services.AddScoped<ICommentInterface, CommentsService>();

builder.Services.AddScoped<IPostService, PostService>();
// registering the base url for the client app
builder.Services.AddHttpClient("Comments", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentApi"])).AddHttpMessageHandler<Util>();
builder.Services.AddCors(options => options.AddPolicy("mypolicy", build =>
{
    //build.WithOrigins("https://localhost:7003");
    build.AllowAnyOrigin();
    build.AllowAnyMethod();
    build.AllowAnyHeader();
}));

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
app.UseCors("mypolicy");
app.MapControllers();

app.Run();
