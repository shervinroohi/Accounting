using AccountingSystem.Api.Middlewares;
using AccountingSystem.Application;
using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.Validators.Transaction;
using AccountingSystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
static string NormalizeKey(string key)
{
    if (key.StartsWith("$."))
        return key[2..];

    return key;
}

static string GetModelStateErrorMessage(
    string key,
    Exception? exception,
    string? errorMessage)
{
    key = NormalizeKey(key);

    if (key.Equals("type", StringComparison.OrdinalIgnoreCase))
        return "Type must be either Payment or Received.";

    if (key.Equals("status", StringComparison.OrdinalIgnoreCase))
        return "Status must be either Settled or UnSettled.";

    if (!string.IsNullOrWhiteSpace(errorMessage))
        return errorMessage;

    return "The provided value is invalid.";
}

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var authorizationHeader =
                    context.Request.Headers.Authorization.FirstOrDefault();


                if (!string.IsNullOrWhiteSpace(authorizationHeader) &&
                    authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authorizationHeader["Bearer".Length..].Trim();

                    if (string.IsNullOrWhiteSpace(token))
                    {
                        context.HttpContext.Items["JwtError"] =
                            "Token is missing.";

                        context.NoResult();
                    }
                }

                return Task.CompletedTask;
            },

            OnAuthenticationFailed = async context =>
            {
                context.NoResult();

                context.HttpContext.Items["JwtError"] =
                    "Invalid or expired token.";

                await Task.CompletedTask;
            },

            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var message =
                    context.HttpContext.Items["JwtError"]?.ToString()
                    ?? "User is not authenticated.";

                var response = new
                {
                    statusCode = 401,
                    message
                };

                await context.Response.WriteAsJsonAsync(response);
            },

            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();



//builder.Services.AddControllers()
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(
//        new System.Text.Json.Serialization.JsonStringEnumConverter());
//});
//;
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x =>
                x.Value?.Errors.Count > 0 &&
                !string.Equals(
                    x.Key,
                    "request",
                    StringComparison.OrdinalIgnoreCase))
            .ToDictionary(
                x => NormalizeKey(x.Key),
                x => x.Value!.Errors
                    .Select(error =>
                        GetModelStateErrorMessage(
                            x.Key,
                            error.Exception,
                            error.ErrorMessage))
                    .ToArray());

        var response = new ErrorResponseDto
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "One or more validation errors occurred.",
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<CreateTransactionExampleFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Accounting API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
