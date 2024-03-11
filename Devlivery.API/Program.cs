using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Aplicacao.Service;
using Devlivery.Infraestrutura;
using Devlivery.API;
using Microsoft.Extensions.Configuration;
using Devlivery.API.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuracao = builder.Configuration;
IServiceCollection servicos = builder.Services;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();
//builder.Services.AddIdentityConfiguration(configuracao);



var startup = new Startup(configuracao, servicos);
var services = Startup.ConfiguraServicos(servicos).BuildServiceProvider();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
