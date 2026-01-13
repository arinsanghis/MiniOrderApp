using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Queries.GetCustomerById;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Required for the fix

var builder = WebApplication.CreateBuilder(args);

// --- 1. Infrastructure Setup ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// --- 2. Application Setup ---
// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
// Register Validators
builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);

// --- 3. API Setup ---
// THE FIX: Add JSON Options to ignore loops
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
