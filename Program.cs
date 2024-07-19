using ClayBackend.Context;
using ClayBackend.Models;
using ClayBackend.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ClayBackend.Entities;
using ClayBackend.Profiles;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using ClayBackend.Services.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Configure swagger to use bearer token
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication dependancies
// I went for bearer for the locks, jwt would have been better but would have taken me too long to implement
builder.Services
    .AddAuthentication(options => {
        //options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        //options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
        options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
    })
    //.AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

// Identity dependancies
builder.Services.AddIdentityCore<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddApiVersioning();

var ass = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Service injection
builder.Services.AddScoped<IDoorRepository, DoorRepository>();
builder.Services.AddScoped<IActivityLoggerService, ActivityLoggerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
