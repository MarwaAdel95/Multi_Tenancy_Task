using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Multi_Tenancy_Task.Data;
using Multi_Tenancy_Task.Repositories;
using Multi_Tenancy_Task.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.
UseNpgsql(builder.Configuration.GetConnectionString("default")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
