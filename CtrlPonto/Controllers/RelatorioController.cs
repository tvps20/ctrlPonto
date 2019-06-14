using CtrlPonto.Models;
using CtrlPonto.Models.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            int tamanhoPagina = 5;
            DateTime _dataInicial;
            DateTime _dataFinal;

            if (dataFinal == null){ _dataInicial = DateTime.Now; }
            else { _dataInicial = DateTime.Parse(dataInicial, new CultureInfo("pt-BR")); }

            if(dataInicial == null){ _dataFinal = DateTime.Now; }
            else { _dataFinal = DateTime.Parse(dataFinal, new CultureInfo("pt-BR")); }

            ViewBag.DataInicial = _dataInicial;
            ViewBag.DataFinal = _dataFinal;

            if (_dataFinal < _dataInicial)
            {
                return View(new List<Trabalho>().ToPagedList(1, 5)).Mensagem("A Data Final não pode ser menor que a Data Inicial.", "Aviso");
            }

            List<Trabalho> trabalhos = TrabalhoRepository.listaAll();

            trabalhos = trabalhos.Where(t => t.Data.Date >= _dataInicial.Date && t.Data.Date <= _dataFinal.Date).ToList();

            TimeSpan Horas = this.calculaHorasTrabalhadas(trabalhos);
            TimeSpan Saldo = this.calculaSaldo(trabalhos);

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

            ViewBag.Horas = Horas;
            ViewBag.Saldo = Saldo;
            return View(trabalhosPaged);
        }

        private TimeSpan calculaHorasTrabalhadas(List<Trabalho> trabalhos)
        {
            TimeSpan horasTrabalhadas = new TimeSpan(0, 0, 0);

            trabalhos.ForEach(x => horasTrabalhadas = horasTrabalhadas.Add(x.Horas));     

            return horasTrabalhadas;
        }

        private TimeSpan calculaSaldo(List<Trabalho> trabalhos)
        {
            TimeSpan saldo = new TimeSpan(0, 0, 0);

            trabalhos.ForEach(x => saldo = saldo.Add(x.Saldo));

            return saldo;
        }

    }
}