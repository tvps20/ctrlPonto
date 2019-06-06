using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CtrlPonto.Models.Domain;

namespace CtrlPonto.Models.Repository
{
    public class TrabalhoRepository
    {
        public static List<Trabalho> listaAll()
        {
            using (var db = new ContextoDB())
            {
                List<Trabalho> trabalhos = db.Trabalhos.SqlQuery("SELECT * FROM trabalhos").ToList();
                return trabalhos;
            }           
        }

        public static Trabalho recuperarPeloId(int id)
        {
            using (var db = new ContextoDB())
            {
                return db.Trabalhos.Find(id);
            }
        }

        public static Trabalho recuperarPelaData(string data)
        {
            using (var db = new ContextoDB())
            {              
                Trabalho trabalho = db.Trabalhos.SqlQuery("SELECT * FROM trabalhos where data = '" + data + "'").FirstOrDefault();
                return trabalho;

            }
        }

        public static int salvar(Trabalho trabalho)
        {
            using (var db = new ContextoDB())
            {
                db.Trabalhos.Add(trabalho);
                return db.SaveChanges();               
            }
        }

        public static bool excluirPeloId(int id)
        {
            using (var db = new ContextoDB())
            {
                var trabalho = new Trabalho { Id = id};
                db.Trabalhos.Attach(trabalho);
                db.Entry(trabalho).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}