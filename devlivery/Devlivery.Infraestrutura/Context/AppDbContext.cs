using Devlivery.Infraestrutura.Map;
using Devlivery.Model.Domain.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace Devlivery.Infraestrutura;

public class AppDbContext : IdentityDbContext<Usuario>
{
    private readonly IConfiguration _config;
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IConfiguration config
        ) : base(options) {

        _config = config;
    }
    // DbSet para a classe de usuário
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Amizade> Amizades { get; set; }
    public DbSet<Projeto> Projeto { get; set; }
    public DbSet<Negocio> Negocios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Servico> Servicos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}










//

//builder.ApplyConfiguration(new UsuarioMap());
/*        builder.Entity<Negocio>()
    .HasOne(a => a.UsuarioResponsavel)
    .WithMany()
    .HasForeignKey(p => p.UsuarioResponsavel);  
//        /
builder.Entity<Projeto>()
    .HasOne(a => a.Usuario)
    .WithMany()
    .HasForeignKey(p => p.Usuario);
builder.Entity<Usuario>(new UsuarioMap().Configure);
builder.Entity<ServicoTrabalho>(new ServicoTrabalhoMap().Configure);            
builder.Entity<Produto>(new ProdutoMap().Configure);
*/
