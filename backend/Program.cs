using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Repositories;
using InsurancePortalRegistration.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InsurancePortalDbContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("InsurancePortal")));

builder.Services.AddControllers();
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();