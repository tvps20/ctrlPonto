using System.Collections.Generic;
using System.Data;
using System.Linq;
using CtrlPonto.Models.Domain;
using PagedList;

namespace CtrlPonto.Models.Repository
{
    public class TrabalhoRepository
    {
        public static IPagedList<Trabalho> listaAll(int tamanhoPagina, int? numeroPagina)
        {
            using (var db = new ContextoDB())
            {
                int pagina = numeroPagina ?? 1;

                IPagedList<Trabalho> trabalhos = db.Trabalhos.SqlQuery("SELECT * FROM trabalhos").OrderByDescending(x => x.Data).ToPagedList(pagina, tamanhoPagina);
            
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