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
    public class NegocioMap : IEntityTypeConfiguration<Negocio>
    {
        public void Configure(EntityTypeBuilder<Negocio> builder)
        {
            builder.ToTable("Negocios");
            builder.HasKey(x => x.NegocioId)
                .HasName("NegocioId");
            //builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Setor)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

            builder.HasOne(y => y.Usuario)
                    .WithMany(z => z.Negocios)
                    .HasForeignKey(p => p.UsuarioId);
        }
    }
}