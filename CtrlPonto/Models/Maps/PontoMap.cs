using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models.Maps
{
    public class PontoMap : EntityTypeConfiguration<Ponto>
    {
        public PontoMap()
        {
            ToTable("pontos");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Hora).HasColumnName("hora").IsRequired();
            Property(x => x.Tipo).HasColumnName("tipo").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();

            Property(x => x.IdTrabalho).HasColumnName("idTrabalho").IsRequired();
            HasRequired(x => x.Trabalho).WithMany().HasForeignKey(x => x.IdTrabalho);
        }
    }
}