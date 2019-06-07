using CtrlPonto.Models.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models.Maps
{
    public class TrabalhoMap : EntityTypeConfiguration<Trabalho>
    {
        public TrabalhoMap()
        {
            ToTable("trabalhos");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Data).HasColumnName("data").IsRequired();
            Property(x => x.Jornada).HasColumnName("jornada");
            Property(x => x.Saldo).HasColumnName("saldo");
            Property(x => x.Horas).HasColumnName("horas");
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        }
    }
}