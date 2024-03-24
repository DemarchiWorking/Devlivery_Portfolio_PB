using Devlivery.Infraestrutura;
using Devlivery.API;

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
