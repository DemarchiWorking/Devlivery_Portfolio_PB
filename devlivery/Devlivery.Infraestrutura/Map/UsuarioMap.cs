using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Devlivery.Model.Domain.DAO;

namespace Devlivery.Infraestrutura.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.UsuarioId)
                .HasName("UsuarioId");



            /*
             * 
                        builder.Entity
                mode


             * 
             */

        }
    }
}
