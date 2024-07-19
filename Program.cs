using ClayBackend.Context;
using ClayBackend.Models;
using ClayBackend.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClayBackend.Entities;
using ClayBackend.Profiles;
using Microsoft.AspNetCore.Mvc.Versioning;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;
using ClayBackend.Repos;
using ClayBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication dependencies
builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
        options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
    })
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

// Identity dependencies
builder.Services.AddIdentityCore<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddApiVersioning();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Service injection
builder.Services.AddScoped<IDoorRepository, DoorRepository>();
builder.Services.AddScoped<IActivityLoggerRepository, ActivityLoggerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IActivityLoggerService, ActivityLoggerService>();


var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Custom auto migrates on startup
    app.ApplyMigrations();
}

// Custom Seeding extension methods
app.SeedRoles();
app.SeedAdminUser();
app.SeedStandardUser();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<User>();

app.Run();
