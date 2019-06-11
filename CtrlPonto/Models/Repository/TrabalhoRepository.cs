using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using CtrlPonto.Models.Domain;
using PagedList;

namespace CtrlPonto.Models.Repository
{
    public class TrabalhoRepository
    {
        public static List<Trabalho> listaAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Trabalhos.SqlQuery("SELECT * FROM trabalhos").ToList();
            }           
        }

        public static Trabalho recuperarPeloId(int id)
        {
            using (var db = new ContextoDB())
            {
                return db.Trabalhos.Find(id);
            }
        }

        public static Trabalho recuperarPelaData(DateTime data)
        {
            using (var db = new ContextoDB())
            {
                return db.Trabalhos.Where(c=> EntityFunctions.TruncateTime(c.Data).Value == data.Date).FirstOrDefault();  
            }
        }

        public static int salvar(Trabalho trabalho)
        {
            using (var db = new ContextoDB())
            {
                var trabalhoBd = recuperarPeloId(trabalho.Id);

                if (trabalhoBd == null)
                {
                    db.Trabalhos.Add(trabalho);
                }
                else
                {
                    db.Trabalhos.Attach(trabalho);
                    db.Entry(trabalho).State = EntityState.Modified;
                }

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