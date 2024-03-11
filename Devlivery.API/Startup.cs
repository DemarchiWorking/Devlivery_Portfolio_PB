using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Devlivery.Infraestrutura;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Hosting;
using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Aplicacao.Service;
using Devlivery.Model.Domain.DAO;
using Microsoft.Extensions.DependencyInjection;
namespace Devlivery.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IServiceCollection Services { get; }
        public Startup(
            IConfiguration configuration,
            IServiceCollection services)
        {
            var test = ObterAppsettings();


        }
        public static IServiceCollection ConfiguraServicos(IServiceCollection services)
        {
            var appsettings = ObterAppsettings();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(appsettings.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

 

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "seu-issuer",
                    ValidAudience = "seu-audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("antonio-teste"))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }

        public static IConfiguration ObterAppsettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               //.SetBasePath(hostEnvironment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               //.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .Build();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwaggerUI();
            }


        }
    }
}










/*
 * 
 app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
endpoints.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
});
 */           //services.CreateAsync(new IdentityRole("Iniciante"));
              //services.AddIdentity<IdentityUser, IdentityRole>()
              //    .AddEntityFrameworkStores<AppDbContext>()
              //    .AddDefaultTokenProviders();
/*
var IHostEnvironment hostEnvironment = 
if (hostEnvironment.IsDevelopment() || hostEnvironment.IsStaging() || hostEnvironment.IsProduction())
{
            using(var scoped = app.ApplicationServices.CreateScope())
            {
                var gerenciarRoles = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Investidor", "Aluno", "Usuario" };

                foreach(var role in roles)
                {
                    if (!await gerenciarRoles.RoleExistsAsync(role))
                        await gerenciarRoles.CreateAsync(new IdentityRole(role));
                }
            }
}Configuration = builder.Build(); */
