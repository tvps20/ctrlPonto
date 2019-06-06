using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CtrlPonto.Models.Enuns;
using CtrlPonto.Models.Maps;

namespace CtrlPonto.Models.Domain
{
    public class ContextoDB : DbContext
    {
        public ContextoDB() : base("name=principal")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TrabalhoMap());
            modelBuilder.Configurations.Add(new PontoMap());
        }

        public DbSet<Trabalho> Trabalhos { get; set; }
        public DbSet<Ponto> Pontos { get; set; }
    }
}