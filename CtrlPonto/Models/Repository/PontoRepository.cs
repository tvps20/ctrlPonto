using CtrlPonto.Models.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CtrlPonto.Models.Repository
{
    public class PontoRepository
    {
        public static List<Ponto> listAllByTrabalho(int id)
        {
            using (var db = new ContextoDB())
            {
                List<Ponto> pontos = db.Pontos.SqlQuery("SELECT * FROM pontos WHERE idTrabalho = " + id).ToList();
                return pontos;
            }
        }

        public static Ponto recuperarPeloId(int id)
        {
            using (var db = new ContextoDB())
            {
                return db.Pontos.Find(id);
            }
        }

        public static int salvar(Ponto ponto)
        {
            using (var db = new ContextoDB())
            {
                db.Pontos.Add(ponto);
                return db.SaveChanges();
            }
        }

        public static bool excluirPeloId(int id)
        {
            using (var db = new ContextoDB())
            {
                var ponto = new Ponto { Id = id };
                db.Pontos.Attach(ponto);
                db.Entry(ponto).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}