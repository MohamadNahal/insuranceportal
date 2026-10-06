using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Repositories;
using InsurancePortalRegistration.Services;
using Microsoft.EntityFrameworkCore;
using InsurancePortalRegistration.ExceptionHandling;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InsurancePortalDbContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("InsurancePortal")));

builder.Services.AddControllers();
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("Login", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString()
                ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));

    options.RejectionStatusCode =
        StatusCodes.Status429TooManyRequests;
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IPolicyholderRepository,SqlPolicyholderRepository>();

builder.Services.AddScoped<IAccountRepository,SqlAccountRepository>();

builder.Services.AddScoped<IContactRepository,SqlContactRepository>();

builder.Services.AddScoped<ISecurityInformationRepository,SqlSecurityInformationRepository>();

builder.Services.AddScoped<IRegistrationService,RegistrationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRegistrationRepository, SqlRegistrationRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler();
app.UseRateLimiter();
app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();