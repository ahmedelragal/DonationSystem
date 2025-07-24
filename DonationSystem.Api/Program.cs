using AutoMapper;
using DonationSystem.Api.Middlewares;
using DonationSystem.Application;
using DonationSystem.Application.MappingProfiles;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Infrastructure;
using DonationSystem.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(Assembly.Load("DonationSystem.Application"));
});

builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = "<eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg0Njc4NDAwIiwiaWF0IjoiMTc1MzE4NzUzMCIsImFjY291bnRfaWQiOiIwMTk4MzIxZTBlY2U3MTE4YWIzMzY1MDdkYjRkYWJmYyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazBzMXhkajVieTFwMGt4cm1icXl3dzVkIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.aoTZ6UjSdqEuTt0DzZSiHKR0yR03XL2SPX0LqqoqYKlXZpcn79_QpVcV09oPS00BukxeCo9fuzAwRDbNUWYUsp0gknOm6iNt9l8vLxH0nNGD-dgWIkgefngrWrJb7hhtAbAuFZO42_BKEQbeu06vIShBTgjie-d7R5Mdnm4smUU8ACIyDnU7D0yfB-NEZiVIgerVDhZPLq2wnZljl-QJPO36z4-nYaw2_DmNtlQzh5gNSJChdHkY-J_Ay2U_JsP_vusw8F6DMY6v_VBAUHpq_tubot9cVIOfF4G56Prj3xmd65mC0Mgaen6WP47uy_GXbYM9EiYgsbkB-Gozy_e-FA>", typeof(DonationProfile).Assembly);


// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)),
        ClockSkew = TimeSpan.Zero
    };
});
// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Donation System", Version = "v1" });

    // 🔐 Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token like this: Bearer {your token here}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RateLimitingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
