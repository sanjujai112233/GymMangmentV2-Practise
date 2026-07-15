using GymMangV2.Application.Interfaces;
using GymMangV2.Infrastructure.DbBridge;
using GymMangV2.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymMangV2.Application.Interfaces.Services;
using GymMangV2.Infrastructure.Services;
using GymMangV2.Application.Interfaces.Security;
using Microsoft.OpenApi.Models;
using GymMangV2.Application.Interfaces;
using GymMangV2.Infrastructure.Repositories;
using GymMangV2.Application.Service;
using GymMangV2.api.Middelware;
using GymMangV2.Application.Mappings;
using GymMangV2.api.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

//------- configration
builder.Services.AddDbContext<GymDbContext>(options =>
{
    options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtSetting>(
    builder.Configuration.GetSection("Jwt")
);

//-----------Service 

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthServices,AuthService>();
builder.Services.AddScoped<IMemberRepository,MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
//builder.Services.AddScoped<MembershipRepository>();
builder.Services.AddAutoMapper(typeof(MemberProfile));
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"JWT auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"JWT challenge: {context.Error} {context.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"JWT validated for: {context.Principal?.Identity?.Name}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHostedService<MembershipExpiryBackgroundService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gym Management API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.UseAuthentication();
app.UseAuthorization(); //Authorization needs HttpContext.User (who calling the checking System)

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

