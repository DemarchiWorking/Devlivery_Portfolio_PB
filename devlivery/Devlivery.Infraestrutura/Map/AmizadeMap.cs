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
    public class AmizadeMap : IEntityTypeConfiguration<Amizade>
    {
        public void Configure(EntityTypeBuilder<Amizade> builder)
        {
            builder.ToTable("Amizades");

            builder.HasKey(x => x.AmizadeId)
                .HasName("AmizadeId");

            builder.HasOne(y => y.UsuarioEnviou)
                 .WithMany(z => z.AmizadesEnviadas)
                 .HasForeignKey(p => p.UsuarioEnviouId)
                 .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(y => y.UsuarioRecebeu)
                 .WithMany(z => z.AmizadesRecebidas)
                 .HasForeignKey(p => p.UsuarioRecebeuId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }
}