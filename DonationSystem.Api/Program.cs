using DonationSystem.Application;
using DonationSystem.Application.Behaviors;
using DonationSystem.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DonationSystem.Application.MappingProfiles;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(Assembly.Load("DonationSystem.Application"));
});

builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = "<eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg0Njc4NDAwIiwiaWF0IjoiMTc1MzE4NzUzMCIsImFjY291bnRfaWQiOiIwMTk4MzIxZTBlY2U3MTE4YWIzMzY1MDdkYjRkYWJmYyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazBzMXhkajVieTFwMGt4cm1icXl3dzVkIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.aoTZ6UjSdqEuTt0DzZSiHKR0yR03XL2SPX0LqqoqYKlXZpcn79_QpVcV09oPS00BukxeCo9fuzAwRDbNUWYUsp0gknOm6iNt9l8vLxH0nNGD-dgWIkgefngrWrJb7hhtAbAuFZO42_BKEQbeu06vIShBTgjie-d7R5Mdnm4smUU8ACIyDnU7D0yfB-NEZiVIgerVDhZPLq2wnZljl-QJPO36z4-nYaw2_DmNtlQzh5gNSJChdHkY-J_Ay2U_JsP_vusw8F6DMY6v_VBAUHpq_tubot9cVIOfF4G56Prj3xmd65mC0Mgaen6WP47uy_GXbYM9EiYgsbkB-Gozy_e-FA>", typeof(DonationProfile).Assembly);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Donation API", Version = "v1" });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
