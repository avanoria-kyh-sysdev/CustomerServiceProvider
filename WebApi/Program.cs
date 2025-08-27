using Microsoft.EntityFrameworkCore;
using WebApi.Data.Contexts;
using WebApi.Data.Repositories;
using WebApi.Grpc;
using WebApi.Interfaces;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDatabase")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerInformationRepository, CustomerInformationRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<CustomerGrpcManager>();

app.Run();
