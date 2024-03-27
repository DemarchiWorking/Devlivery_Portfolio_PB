using Devlivery.Infraestrutura;
using Devlivery.API;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuracao = builder.Configuration;
IServiceCollection servicos = builder.Services;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "DiretoAoPonto - Identity API",
        Description = "Esta API serve para consumir a API de Identidade do DiretoAoPonto-Uow c/JWT.",
        Contact = new OpenApiContact() { Name = "Carlos A Santos", Email = "carlos.itdeveloper@gmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme"
        //.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.
        //\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
        new string[]{}
      }
    });
});

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();
//builder.Services.AddIdentityConfiguration(configuracao);



var startup = new Startup(configuracao, servicos);
var services = Startup.ConfiguraServicos(servicos).BuildServiceProvider();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true) 
.AllowCredentials());

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
