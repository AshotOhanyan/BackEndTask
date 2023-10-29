using Data.Data;
using Data.DbModels;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Services.Services.ClassAssignmentServices;
using Services.Services.ClassServices;
using Services.Services.UserServices;
using Services.StaticContent;

using System.Reflection;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

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
                         new string[] {}
                    }
                });
});

string? connectionString = Environment.GetEnvironmentVariable("MY_DATABASE_CONNECTIONSTRING", EnvironmentVariableTarget.Machine);


builder.Services.AddDbContext<DBContext>(options =>
        options.UseSqlServer(connectionString),ServiceLifetime.Transient,ServiceLifetime.Transient);


builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IRepository<Class>, ClassRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<IClassAssignmentService, ClassAssignmentService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Constants.ISSUER,
            ValidateAudience = true,
            ValidAudience = Constants.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = Constants.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,

        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<JwtRefreshMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();
