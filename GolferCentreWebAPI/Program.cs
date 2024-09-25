using GolferCentreWebAPI.DTO.Golfer;
using GolferCentreWebAPI.Models;
using GolferCentreWebAPI.Service;
using GolferCentreWebAPI.Service.Course;
using GolferCentreWebAPI.Service.Golfer;
using GolferCentreWebAPI.Service.Score;
using GolferCentreWebAPI.Service.Tournament;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GolferGoContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("GolferGo")));

builder.Services.AddScoped<IGolferService, GolferService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
