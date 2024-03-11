using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Infraestrutura.Map
{
    public class PropostaMap : IEntityTypeConfiguration<Proposta>
    {
        public void Configure(EntityTypeBuilder<Proposta> builder)
        {

            builder.ToTable("Propostas");

            builder.HasKey(x => x.PropostaId)
                .HasName("PropostaId");

            builder.HasOne(y => y.UsuarioEnvia)
                 .WithMany(z => z.PropostasEnviadas)
                 .HasForeignKey(p => p.UsuarioEnviaId)
                 .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(y => y.UsuarioRecebe)
                 .WithMany(z => z.PropostasRecebidas)
                 .HasForeignKey(p => p.UsuarioRecebeId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
/*
    public class Proposta
{
    public Guid PropostaId { get; set; }
    public string Titulo { get; set; }
    public CategoriaProposta Categoria { get; set; }

    public string Descricao { get; set; }
    public float Valor { get; set; }
    public bool Status { get; set; }
    public Guid UsuarioEnviaId { get; set; }
    public Guid UsuarioRecebeId { get; set; }
    public virtual Usuario UsuarioEnvia { get; set; }
    public virtual Usuario UsuarioRecebe { get; set; }

}
/*
public void Configure(EntityTypeBuilder<Proposta> builder)
{

    builder.ToTable("Propostas");

    builder.HasKey(x => x.PropostaId)
        .HasName("PropostaId");

    builder.HasOne(y => y.UsuarioEnvia)
         .WithMany(z => z.PropostasEnviadas)
         .HasForeignKey(p => p.UsuarioEnviaId)
         .OnDelete(DeleteBehavior.NoAction);


    builder.HasOne(y => y.UsuarioRecebe)
         .WithMany(z => z.PropostasRecebidas)
         .HasForeignKey(p => p.UsuarioRecebeId)
         .OnDelete(DeleteBehavior.NoAction);

}
    }
}*/