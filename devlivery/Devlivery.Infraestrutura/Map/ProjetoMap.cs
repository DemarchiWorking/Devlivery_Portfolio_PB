using Devlivery.Model.Domain.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Devlivery.Infraestrutura.Map
{

    public class ProjetoMap : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.ToTable("Projetos");
            builder.HasKey(x => x.ProjetoId)
                .HasName("ProjetoId");
            //builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Titulo)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

            builder.HasOne(y => y.Usuario)
                    .WithMany(z => z.Projetos)
                    .HasForeignKey(p => p.UsuarioId);
        }
    }
}