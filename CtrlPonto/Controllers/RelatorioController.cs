using CtrlPonto.Models;
using CtrlPonto.Models.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlPonto.Controllers
{
    public class RelatorioController : Controller
    {
        [HttpGet]
        public ActionResult Index(string dataInicial, string dataFinal, string sortOrder = "Dia", int pagina = 1)
        {
            Relatorio relatorio = new Relatorio();
            int tamanhoPagina = 10;
            DateTime _dataInicial;
            DateTime _dataFinal;           

            if (dataFinal == null){ _dataInicial = DateTime.Now; }
            else { _dataInicial = DateTime.Parse(dataInicial, new CultureInfo("pt-BR")); }

            if(dataInicial == null){ _dataFinal = DateTime.Now; }
            else { _dataFinal = DateTime.Parse(dataFinal, new CultureInfo("pt-BR")); }

            relatorio.DataInicial = _dataInicial;
            relatorio.DataFinal = _dataFinal;

            if (_dataFinal < _dataInicial)
            {
                return View(new List<Trabalho>().ToPagedList(1, 5)).Mensagem("A Data Final não pode ser menor que a Data Inicial.", "Aviso");
            }

            List<Trabalho> trabalhos = TrabalhoRepository.listaAll();

            trabalhos = trabalhos.Where(t => t.Data.Date >= _dataInicial.Date && t.Data.Date <= _dataFinal.Date).ToList();

            relatorio.Horas = DataUtil.calculaHorasTrabalhadas(trabalhos);
            relatorio.Saldo = DataUtil.calculaSaldo(trabalhos);
            relatorio.HorasFormatada = DataUtil.formataHora(relatorio.Horas);
            relatorio.SaldoFormatado = DataUtil.formataHora(relatorio.Saldo);

            switch (sortOrder)
            {
                case "Dia":
                    trabalhos = trabalhos.OrderByDescending(t => t.Data).ToList();
                    break;
                case "Jornada":
                    trabalhos = trabalhos.OrderBy(t => t.Jornada).ToList();
                    break;
                case "Saldo":
                    trabalhos = trabalhos.OrderBy(t => t.Saldo).ToList();
                    break;
                default:
                    trabalhos = trabalhos.OrderByDescending(t => t.Data).ToList();
                    break;
            }

            IPagedList trabalhosPaged = trabalhos.ToPagedList(pagina, tamanhoPagina);

            relatorio.Trabalhos = trabalhos;
            ViewBag.TrabalhosPaged = trabalhosPaged;
            ViewBag.active = "Relatorio";
            return View(relatorio);
        }

        [HttpPost]
        public void ExportToCsv(List<Trabalho> Trabalhos, string SaldoFormatado, string HorasFormatada, string DataInicial, string DataFinal)
        {
            StringWriter sw = new StringWriter();
            string filename = DateTime.Now.ToString("ddMMyyyyHHmmss");

            sw.WriteLine("Periodo; \"{0}\" - \"{1}\"", DataInicial, DataFinal);
            sw.WriteLine();
            sw.WriteLine("Dia; Jornada; Saldo");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ponto" + filename + ".csv");
            Response.ContentType = "tex/csv";

            Trabalhos.ForEach(t =>
            {
                sw.WriteLine(string.Format("\"{0}\"; \"{1}\"; \"{2}\"", t.Data.ToString("dd/MM/yyyy"), t.Jornada, t.Saldo));
            });

            sw.WriteLine();           
            sw.WriteLine("Horas Trabalhadas; \"{0}\"", HorasFormatada);
            sw.WriteLine("Jornada Total; \"{0}\"", DataUtil.formataHora(DataUtil.calculaJornada(Trabalhos)));
            sw.WriteLine("Saldo Total; \"{0}\"", SaldoFormatado);

            Response.Write(sw.ToString());
            Response.End();
        } 
    }
}